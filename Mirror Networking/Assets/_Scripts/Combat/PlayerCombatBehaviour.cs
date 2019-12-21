using UnityEngine;

[RequireComponent(typeof(InputReader))]
public class PlayerCombatBehaviour : MonoBehaviour
{
    [SerializeField]
    private float damageRange = 2f, damageAmount = 10f;

    [SerializeField]
    private LayerMask damageableObjectsLayer = -1;

    private Ray ray;
    private Camera cam;
    private InputReader _inputReader;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        cam = Camera.main;
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (_inputReader.DamageKeyDown())
        {
            DealDamage();
        }
    }

    private void DealDamage()
    {
        ray = cam.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit, damageRange, damageableObjectsLayer))
        {
            if(TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamge(damageAmount);
            }
        }
    }
}
