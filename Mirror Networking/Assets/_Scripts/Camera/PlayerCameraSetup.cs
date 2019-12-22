using Mirror;
using UnityEngine;

public class PlayerCameraSetup : NetworkBehaviour
{
    [SerializeField]
    private Transform _cameraTarget = null;

    [SerializeField]
    private GameObject _camera = null;

    public Camera Cam { get; private set; }
    public CameraController CamController { get; private set; }
    
    public override void OnStartLocalPlayer()
    {
        GameObject cam = Instantiate(_camera);
        Cam = cam.GetComponent<Camera>();
        CamController = cam.GetComponent<CameraController>();

        CamController.SetTarget(_cameraTarget);
    }
}
