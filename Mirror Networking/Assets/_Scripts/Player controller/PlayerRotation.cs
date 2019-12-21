using Mirror;
using UnityEngine;

[RequireComponent(typeof(InputReader))]
public class PlayerRotation : NetworkBehaviour
{
    [SerializeField]
    private float rotationSpeed = 100f;

    private Vector3 rotation;
    private InputReader _inputReader;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
    }

    private void Update()
    {
        if (!isLocalPlayer)
            return;

        rotation = _inputReader.GetRotation();
    }

    private void FixedUpdate()
    {
        if (!isLocalPlayer)
            return;

        Rotate();
    }

    private void Rotate()
    {
        transform.Rotate(rotation * Time.deltaTime * rotationSpeed);
    }
}
