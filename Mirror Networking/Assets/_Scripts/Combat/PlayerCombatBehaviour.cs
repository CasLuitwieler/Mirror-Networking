using Mirror;
using UnityEngine;

[RequireComponent(typeof(InputReader))]
public class PlayerCombatBehaviour : NetworkBehaviour
{
    [SerializeField]
    private float _hitRange = 2f, _damageAmount = 10f;

    [SerializeField]
    private LayerMask _damageableObjectsLayer = -1;

    private Ray _ray;
    private Camera _cam;
    private CameraController _camController;
    private InputReader _inputReader;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
    }

    private void Start()
    {
        if (!isLocalPlayer)
            return;

        _cam = FindObjectOfType<Camera>();
        _camController = FindObjectOfType<CameraController>();
    }

    private void Update()
    {
        if (!isLocalPlayer)
            return;

        _ray = _cam.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(_ray.origin, _ray.direction * (_hitRange + _camController.DistanceToTarget));

        HandleInput();
    }

    private void HandleInput()
    {
        if (_inputReader.DamageKeyDown())
        {
            _ray = _cam.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(_ray, out RaycastHit hit, _hitRange + _camController.DistanceToTarget, _damageableObjectsLayer))
                return;

            Debug.Log("hit object " + hit.transform.gameObject.name);

            if (hit.transform.TryGetComponent(out NetworkIdentity networkID))
            {
                //damage the object that was hit
                CmdDealDamage(networkID);
            }
            else
            {
                if (hit.transform.TryGetComponent(out IDamageable damageable))
                {
                    Debug.LogError("Dealing damage to an object that doesn't have a network identity");
                    damageable.TakeDamge(_damageAmount);
                }
            }
        }
    }

    [Command]
    private void CmdDealDamage(NetworkIdentity networkID)
    {
        GameObject target = NetworkIDManager.GetPlayer(networkID.netId.ToString());

        if (target.TryGetComponent(out IDamageable damageable))
        {
            Debug.Log("Found IDamageable");
            damageable.TakeDamge(_damageAmount);
        }
    }
}
