using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Mirror;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image foregroundImage = null;

    [SerializeField]
    private float changeHealthTweenDuration = 1f;

    private NetworkIdentity _networkIdentity;

    private void Awake()
    {
        _networkIdentity = GetComponentInParent<NetworkIdentity>();
    }

    private void Start()
    {
        GetComponentInParent<PlayerHealthBehaviour>().EventHealthChanged += (x) => HandleHealthChanged(x);
        if (_networkIdentity.isLocalPlayer)
            gameObject.SetActive(false);
    }

    private void HandleHealthChanged(float targetPercentage)
    {
        foregroundImage.DOFillAmount(targetPercentage, changeHealthTweenDuration);
    }
}
