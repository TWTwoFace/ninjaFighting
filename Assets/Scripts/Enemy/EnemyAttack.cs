using System;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
		[SerializeField] private float attackRange;
		[SerializeField] private float attackCooldown;
		[SerializeField] private int damage;
		[SerializeField] private bool displayAttackRange;
		private float timeTilNextAttack;

		public event Action AttackPerformed;

		public void Attack()
		{
				Ray ray = new Ray(transform.position, transform.forward);
				Physics.Raycast(ray, out RaycastHit hit, attackRange);
				if (hit.collider)
				{
						if (hit.collider.TryGetComponent(out PlayerHealth playerHealth))
						{
								playerHealth.TakeDamage(damage);
								AttackPerformed?.Invoke();
						}
				}
		}
}