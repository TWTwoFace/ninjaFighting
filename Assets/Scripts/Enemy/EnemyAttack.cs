using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAttack : MonoBehaviour
{
	public event Action Attacked;
	public event Action Started;

	[SerializeField] private float _timeToAttack;

	private const string _triggerName = "Attack";

	private Animator _animator;

	private float _timer;
	private bool _canAttack;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
	}

	private void Update()
	{
		DetermineCanAttackByTimer();
	}

	private void DetermineCanAttackByTimer()
	{
		if (_timer >= _timeToAttack)
		{
			_canAttack = true;
			return;
		}
		_timer += Time.deltaTime;
		_canAttack = false;
	}

	public void Attack()
	{
		if (_canAttack == false)
		{
			OnAttackEnd();
			return;
		}

		_animator.SetTrigger(_triggerName);
		Started?.Invoke();
	}

	public void OnAttackEnd()
	{
		Attacked?.Invoke();
	}
}