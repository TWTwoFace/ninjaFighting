using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public event Action<int> HealthChanged;
    public event Action Dead;

    [SerializeField] private int _maxHealth;

    private int _health;

    private void Start()
    {
        _health = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0)
            return;

        _health = Math.Clamp(_health - damage, 0, _maxHealth);

        HealthChanged?.Invoke(_health);

        if(_health <= 0)
        {
            Dead?.Invoke();
        }
    }

    public void Heal(int healValue)
    {
        if (healValue < 0)
            return;

        _health = Math.Clamp(_health + healValue, 0, _maxHealth);

        HealthChanged?.Invoke(_health);
    }
}
