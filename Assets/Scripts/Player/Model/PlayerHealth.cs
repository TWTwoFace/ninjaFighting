using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public event Action CanTakeDamage;
    public event Action<int> HealthChanged;
    public event Action Hitted;
    public event Action Dead;

    [SerializeField] private int _maxHealth;

    private int _health;

    private bool _canGetDamage = true;

    private Coroutine _healRoutine;

    private void Start()
    {
        _health = _maxHealth;
        _healRoutine = StartCoroutine(HealRoutine());
    }

    public void TakeDamage(int damage)
    {
        if (enabled == false)
            return;

        if (damage < 0)
            return;

        if (_canGetDamage == false)
            return;

        _health = Math.Clamp(_health - damage, 0, _maxHealth);

        HealthChanged?.Invoke(_health);
        Hitted?.Invoke();

        if(_health <= 0)
        {
            Dead?.Invoke();
            _canGetDamage = false;
            return;
        }

        StartCoroutine(OnTakeDamageRoutine());
    }

    public void Heal(int healValue)
    {
        if (healValue < 0)
            return;

        _health = Math.Clamp(_health + healValue, 0, _maxHealth);

        HealthChanged?.Invoke(_health);

    }

    private IEnumerator OnTakeDamageRoutine()
    {
        _canGetDamage = false;
        yield return new WaitForSeconds(0.2f);
        _canGetDamage = true;
        CanTakeDamage?.Invoke();
    }

    private IEnumerator HealRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Heal(1);
        }
    }

    private void OnDead()
    {
        StopCoroutine(_healRoutine);
    }

    private void OnEnable()
    {
        Dead += OnDead;
    }

    private void OnDisable()
    {
        Dead -= OnDead;
    }
}
