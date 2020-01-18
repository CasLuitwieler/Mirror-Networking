using UnityEngine;

public class HealthSystemBehaviour : MonoBehaviour, IDamageable, IHealable
{
    [SerializeField]
    private int _maxHealth = 100;

    private HealthSystem _healthSystem;
    public HealthSystem HealthSystem
    {
        get
        {
            if (_healthSystem != null) { return _healthSystem; }
            _healthSystem = new HealthSystem(_maxHealth);
            return _healthSystem;
        }
    }

    public void TakeDamge(float damageAmount)
    {
        _healthSystem.TakeDamge(damageAmount);
    }

    public void Heal(int healAmount)
    {
        _healthSystem.Heal(healAmount);
    }
}
