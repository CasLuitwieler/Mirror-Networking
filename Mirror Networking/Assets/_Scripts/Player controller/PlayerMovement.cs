using Mirror;
using UnityEngine;

[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : NetworkBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;

    private Vector3 _moveDirection;
    private InputReader _inputReader;
    private CharacterController _controller;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (!isLocalPlayer)
            return;

        _moveDirection = _inputReader.GetMoveDirection();
    }

    private void FixedUpdate()
    {
        if (!isLocalPlayer)
            return;

        Move();
    }

    private void Move()
    {
        Vector3 move = _moveDirection.x * transform.right + _moveDirection.z * transform.forward;
        _controller.Move(move * moveSpeed * Time.deltaTime);
    }
}
