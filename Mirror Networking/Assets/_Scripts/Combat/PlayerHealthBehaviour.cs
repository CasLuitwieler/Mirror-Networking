using Mirror;
using System;
using UnityEngine;

public class PlayerHealthBehaviour : NetworkBehaviour, IDamageable
{
    public delegate void OnPlayerDied();
    public delegate void OnHealthChanged(float healthPercentage);

    //TODO: when new player joins, show the correct health value
    [SyncEvent]
    public event OnHealthChanged EventHealthChanged;

    [SyncEvent]
    public event OnPlayerDied EventPlayerDied;

    [SyncVar]
    private float _health;

    [SerializeField]
    private float _maxHealth = 100f;

    private bool isDead = false;
    private HealthBar _healthBar;

    private void Awake()
    {
        _health = _maxHealth;
    }

    public void TakeDamge(float damageAmount)
    {
        if (isDead)
            return;

        _health -= damageAmount;
        float healthPercentage = _health / _maxHealth;
        EventHealthChanged?.Invoke(healthPercentage);

        if (_health <= 0)
        {
            isDead = true;
            EventPlayerDied?.Invoke();
            DestroyAuthorisedObjects(GetComponent<NetworkIdentity>());
        }
    }

    private void DestroyAuthorisedObjects(NetworkIdentity networkIdentity)
    {
        //NetworkServer.Destroy(this.gameObject);
        NetworkServer.DestroyPlayerForConnection(networkIdentity.connectionToClient);
    }
}
