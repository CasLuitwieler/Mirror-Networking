using System;
using UnityEngine;

public class HealthChangedArgs : EventArgs
{
    public float Health { get; }
    public float MaxHealth { get; }

    public HealthChangedArgs(float health, float maxHealth)
    {
        Health = health;
        MaxHealth = maxHealth;
    }
}
