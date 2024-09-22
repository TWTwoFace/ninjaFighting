using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAttack : MonoBehaviour
{
	public event Action AttackPerformed;
	public event Action AttackStarted;

	private const string _triggerName = "Attack";

	private Animator _animator;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
	}

	public void Attack()
	{
		_animator.SetTrigger(_triggerName);

		AttackStarted?.Invoke();
    }

	public void OnAttackEnd()
	{
		AttackPerformed?.Invoke();
	}
}