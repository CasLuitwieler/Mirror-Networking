using Mirror;
using System;
using UnityEngine;

public class PlayerHealthBehaviour : NetworkBehaviour, IDamageable
{
    public static event Action OnPlayerDied;

    [SerializeField]
    private float _maxHealth = 100f;

    [SyncVar]
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

        Debug.Log("Before: " + _health);
        _health -= damageAmount;
        Debug.Log("After: " + _health);

        if (_health <= 0)
        {
            isDead = true;
            OnPlayerDied?.Invoke();
            Debug.Log(gameObject.name + " died");
            Destroy(this.gameObject);
        }
    }

    /*
    [TargetRpc]
    public void TargetTakeDamge(NetworkIdentity networkID, float damageAmount)
    {
        if (isDead)
            return;

        _health -= damageAmount;
        if(_health <= 0)
        {
            isDead = true;
            OnPlayerDied?.Invoke();
            Debug.Log(gameObject.name + " died");
            Destroy(this.gameObject);
        }
    }
    */
}
