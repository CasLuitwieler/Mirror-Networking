using Mirror;
using UnityEngine;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    private Transform _cameraTarget = null;

    private CameraController _cam;

    public override void OnStartLocalPlayer()
    {
        _cam = FindObjectOfType<CameraController>();
        _cam.SetTarget(_cameraTarget);
    }

    private void OnDestroy()
    {
        if(isLocalPlayer)
            _cam.SetTarget(null);
    }
}
