using UnityEngine;

public class Enemy : MonoBehaviour
{
        private enum EnemyState
        {
                Moving,
                Attacking,
        }

        [SerializeField] private EnemyAgent agent;
        
        [Header("Movement"), Space]
        [SerializeField] private float followRange;
        [SerializeField] private float stopDistance;
        [SerializeField] private bool displayFollowRange;
        
        [Header("Attacking"), Space]
        [SerializeField] private float attackRange;
        [SerializeField] private float attackCooldown;
        [SerializeField] private float damage;
        [SerializeField] private bool displayAttackRange;
        private float timeTilNextAttack;
        
        private Transform target;
        private EnemyState state;

        public void Init(Transform target)
        {
                this.target = target;
                timeTilNextAttack = -1f;
        }

        private void Dispose()
        {
                this.target = null;
                gameObject.SetActive(false);
        }
        
        private void Update()
        {
                var distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (distanceToTarget < stopDistance)
                {
                        agent.StopMoving();
                }
                if (distanceToTarget < attackRange)
                {
                        if (timeTilNextAttack <= 0f)
                        {
                                Attack();
                                timeTilNextAttack = attackCooldown;
                        }
                        else
                        {
                                timeTilNextAttack -= Time.deltaTime;
                        }
                }
                else
                {
                        Move();
                }
        }

        private void Move()
        {
                agent.Move(target.position - (target.position - transform.position).normalized * stopDistance);
        }

        private void Attack()
        {
                Ray ray = new Ray(agent.transform.position, agent.GetLookDirection());
                Physics.Raycast(ray, out RaycastHit hit, attackRange);
                if (hit.collider)
                {
                        //TODO: сравнение по типу + нанесение урона
                        if (hit.collider.gameObject.CompareTag("Player"))
                        {
                                Debug.Log($"hitting {hit.collider.gameObject.name} with damage:{damage}");
                        }
                }
                
        }
        
        private void OnDrawGizmosSelected()
        {
                if (displayAttackRange)
                {
                        int rayAmount = 8;
                        Gizmos.color = Color.red;
                        for (int i = 0; i < rayAmount; i++)
                        {
                                var angle = Quaternion.Euler(0f, 360f/rayAmount*i, 0f) * agent.transform.forward;
                                Gizmos.DrawLine(agent.transform.position, agent.transform.position + angle * attackRange);
                        }
                }
                if (displayFollowRange)
                {
                        int rayAmount = 16;
                        Gizmos.color = Color.yellow;
                        for (int i = 0; i < rayAmount; i++)
                        {
                                var angle = Quaternion.Euler(0f, 360f/rayAmount*i + 12.25f, 0f) * agent.transform.forward;
                                Gizmos.DrawLine(agent.transform.position, agent.transform.position + angle * followRange);
                        }
                }
        }
}