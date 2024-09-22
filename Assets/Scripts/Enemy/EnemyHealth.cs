using System;
using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
	public event Action<int> HealthChanged;
	public event Action Dead;
	public event Action Damaged;
	public event Action CanTakeDamage;

	[SerializeField] private int _maxHealth;

	private int _health;
	private bool _canGetDamage = true;

	private void Start()
	{
		_health = _maxHealth;
	}

	public void TakeDamage(int damage)
	{
		if (enabled == false)
			return;

		if (_canGetDamage == false)
			return;

		if (damage < 0)
			return;

		_health = Math.Clamp(_health - damage, 0, _maxHealth);

		HealthChanged?.Invoke(_health);

		StartCoroutine(OnTakeDamageRoutine());

		Damaged?.Invoke();

        if (_health <= 0)
		{
			Dead?.Invoke();
		}
	}

	private IEnumerator OnTakeDamageRoutine()
	{
		_canGetDamage = false;
		yield return new WaitForSeconds(0.2f);
		_canGetDamage = true;
		CanTakeDamage?.Invoke();
	}
}