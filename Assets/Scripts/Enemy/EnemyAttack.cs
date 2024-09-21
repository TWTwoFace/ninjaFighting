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

		private void OnDrawGizmosSelected()
		{
				int rayAmount = 8;
				Gizmos.color = Color.red;
				for (int i = 0; i < rayAmount; i++)
				{
					var angle = Quaternion.Euler(0f, 360f/rayAmount*i, 0f) * transform.forward;
					Gizmos.DrawLine(transform.position, transform.position + angle * attackRange);
				}
		}
}