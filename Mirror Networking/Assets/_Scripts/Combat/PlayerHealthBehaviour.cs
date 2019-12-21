using System;
using UnityEngine;

public class PlayerHealthBehaviour : MonoBehaviour, IDamageable
{
    public static event Action OnPlayerDied;

    [SerializeField]
    private float _maxHealth = 100f;

    private float _health;
    private bool isDead = false;

    private void Awake()
    {
        _health = _maxHealth;
    }

    public void TakeDamge(float damageAmount)
    {
        if (isDead)
            return;

        _health -= damageAmount;
        if(_health <= 0)
        {
            isDead = true;
            OnPlayerDied?.Invoke();
            Debug.Log(gameObject.name + " died");
        }
    }
}
