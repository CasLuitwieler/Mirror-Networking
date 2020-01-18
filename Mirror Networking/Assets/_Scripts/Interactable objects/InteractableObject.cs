using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

[RequireComponent(typeof(SphereCollider))]
public class InteractableObject : MonoBehaviour, IInteractable
{
    [SerializeField]
    private float _indicatorRange = 8f, _indicatorFadeDuration = 0.25f;

    [SerializeField]
    private GameObject _indicator = null;
    
    private SphereCollider _indicatorTrigger;
    private Image _indicatorImage;

    private void Awake()
    {
        _indicatorTrigger = GetComponent<SphereCollider>();
        _indicatorTrigger.isTrigger = true;
        _indicatorTrigger.radius = _indicatorRange;

        _indicatorImage = _indicator.GetComponentInChildren<Image>();

        Color color = _indicatorImage.color;
        color.a = 0f;
        _indicatorImage.color = color;
    }

    public void OnStartHover()
    {
        Debug.Log("Start hover");
    }

    public void OnInteract()
    {
        Debug.Log("OnInteract triggered");
    }
    
    public void OnEndHover()
    {
        Debug.Log("End hover");
        
        //if player within range indicator range, enable indicator UI
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out DetectInteractableObjects player))
        {
            //show indicator UI sprite of interactable object for this player
            _indicatorImage.DOFade(1f, _indicatorFadeDuration);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out DetectInteractableObjects player))
        {
            //hide indicator UI sprite of interactable object for this player
            _indicatorImage.DOFade(0f, _indicatorFadeDuration);
        }
    }
}
