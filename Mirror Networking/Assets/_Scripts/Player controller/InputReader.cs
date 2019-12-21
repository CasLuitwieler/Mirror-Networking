using Mirror;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    [SerializeField]
    private KeyCode damageKey = KeyCode.Mouse0;

    private Vector3 direction, rotation;

    public Vector3 GetMoveDirection()
    {
        direction = Vector3.zero;

        direction.x = Input.GetAxisRaw("Horizontal");
        direction.z = Input.GetAxisRaw("Vertical");

        return direction;
    }

    public Vector3 GetRotation()
    {
        rotation = Vector3.zero;

        rotation.y = Input.GetAxis("Mouse X");

        return rotation;
    }

    public bool DamageKeyDown()
    {
        if (Input.GetKeyDown(damageKey))
            return true;
        return false;
    }
}