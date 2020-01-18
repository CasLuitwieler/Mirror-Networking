using System;
using UnityEngine;

public class HealthSystem : IDamageable, IHealable
{
    private readonly int _maxHealth;

    public HealthSystem(int maxHealth)
    {
        _maxHealth = maxHealth;
        Health = maxHealth;
    }

    public event EventHandler<HealthChangedArgs> HealthChanged = delegate { };
    public event EventHandler Died = delegate { };

    public bool IsDead { get; private set; } = false;
    public float Health { get; private set; } = 0;
    public float MaxHealth => _maxHealth;

    public void TakeDamge(float damageAmount)
    {
        if (IsDead) { return; }
        if (damageAmount <= 0) { return; }

        Health = Mathf.Max(0, Health - (int)damageAmount);
        HealthChanged(this, new HealthChangedArgs(Health, MaxHealth));

        if (Health == 0)
        {
            IsDead = true;
            Died(this, EventArgs.Empty);
        }
    }

    public void Heal(int healAmount)
    {
        if (IsDead) { return; }
        if (healAmount <= 0) { return; }

        Health = Mathf.Min(_maxHealth, Health + healAmount);
        HealthChanged(this, new HealthChangedArgs(Health, _maxHealth));
    }
}
