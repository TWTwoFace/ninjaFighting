using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAttack : MonoBehaviour
{
	public event Action AttackPerformed;
	public event Action AttackStarted;

	[SerializeField] private int _damage;
	[SerializeField] private Collider _weaponCollider;

	private const string _triggerName = "Attack";

	private Animator _animator;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
	}

	public void Attack()
	{
		_animator.SetTrigger(_triggerName);

		_weaponCollider.enabled = true;

        AttackStarted?.Invoke();
    }

	public void OnAttackEnd()
	{
        _weaponCollider.enabled = false;

        AttackPerformed?.Invoke();
	}

	private void OnDisable()
	{
		_weaponCollider.enabled = false;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent<PlayerHealth>(out var playerHealth))
		{
			playerHealth.TakeDamage(_damage);
		}
	}
}