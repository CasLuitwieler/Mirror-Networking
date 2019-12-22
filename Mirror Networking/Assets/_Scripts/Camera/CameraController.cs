using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float _xRotationSpeed = 200f, _yRotationSpeed = 200f, _minXRotataion = -65f, _maxXRotation = 75f, _zoomSpeed = 50f, 
        _minDistanceToTarget = 3f, _maxDistanceToTarget = 5f;
    
    public void SetTarget(Transform target) { _target = target; }
    public float DistanceToTarget => _distanceToTarget;

    private float _distanceToTarget, _xRotation, _yRotation, _zoomInput;
    private Vector3 _offset;
    private Transform _target;

    private void Awake()
    {
        _distanceToTarget = 3f;
        _offset = new Vector3(0.75f, 0f, 0f);

        LockCursor();
    }

    private void Update()
    {
        HandleInput();
    }
     
    private void HandleInput()
    {
        _yRotation += Input.GetAxis("Mouse X") * Time.deltaTime * _yRotationSpeed;
        _xRotation += Input.GetAxis("Mouse Y") * Time.deltaTime * _xRotationSpeed * -1f;
        _zoomInput = Input.mouseScrollDelta.y * Time.deltaTime * _zoomSpeed * -1f;

        _xRotation = Mathf.Clamp(_xRotation, _minXRotataion, _maxXRotation);
        _distanceToTarget += _zoomInput;
        _distanceToTarget = Mathf.Clamp(_distanceToTarget, _minDistanceToTarget, _maxDistanceToTarget);

        if (Input.GetKeyDown(KeyCode.T))
            ToggleCursorLockMode();
    }

    private void LateUpdate()
    {
        transform.position = _target.position;                                                                      //move camera to target position
        transform.position += Mathf.Cos(_xRotation * Mathf.Deg2Rad) * _distanceToTarget * _target.forward * -1f +   //move camera back
                              Mathf.Sin(_xRotation * Mathf.Deg2Rad) * _distanceToTarget * _target.up;               //move camera up/down
        transform.LookAt(_target);                                                                                  //look at the player
        transform.position += (_target.right * _offset.x + _target.up * _offset.y);                                 //apply offset
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ToggleCursorLockMode()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
            UnlockCursor();
        else
            LockCursor();
    }
}
