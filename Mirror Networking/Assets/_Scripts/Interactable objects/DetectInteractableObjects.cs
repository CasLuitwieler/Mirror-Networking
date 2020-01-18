using Mirror;
using UnityEngine;

public class DetectInteractableObjects : NetworkBehaviour
{
    [SerializeField]
    private float _interactRange = 3f;

    private IInteractable _currentInteractableTarget;
    private Transform _currentTargetTransform;

    private Ray _ray;
    private Camera _cam;    

    private void Start()
    {
        _cam = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        if (!isLocalPlayer)
            return;

        RaycastForward();
    }

    private void RaycastForward()
    {
        _ray = _cam.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(_ray, out RaycastHit hit, _interactRange))
            EndInteraction();
        else if(_currentTargetTransform == null)                            //start looking at a new object
        {
            StartInteraction(hit.transform);
        }
        else if(_currentTargetTransform != hit.transform)                   //switched looking between objects
        {
            EndInteraction();
            StartInteraction(hit.transform);
        }
    }
    
    private void StartInteraction(Transform newObjectTransform)             //when looking at a new object or entering it's detection range
    {
        _currentTargetTransform = newObjectTransform;
        if (TryGetComponent(out IInteractable interactable))
        {
            _currentInteractableTarget = interactable;
            _currentInteractableTarget.OnStartHover();
        }
    }

    private void EndInteraction()                                           //when looking away from an object or going out range
    {
        if (_currentInteractableTarget == null)
            return;

        _currentInteractableTarget.OnEndHover();
        _currentInteractableTarget = null;
        _currentTargetTransform = null;
    }
}
