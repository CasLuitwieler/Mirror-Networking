using Mirror;
using UnityEngine;

public class CameraBillboard : MonoBehaviour
{
    private Camera _cam;

    private void Start()
    {
        _cam = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        if(_cam != null)
            RotateTowardsCamera();
    }

    private void RotateTowardsCamera()
    {
        transform.LookAt(transform.position + _cam.transform.rotation * Vector3.forward,
            _cam.transform.rotation * Vector3.up);
    }
}
