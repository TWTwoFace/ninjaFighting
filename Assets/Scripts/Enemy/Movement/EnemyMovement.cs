using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float followRange;
    [SerializeField] private float attackRange;
    [SerializeField] private float stopDistance;
    [SerializeField] private float stepAwayDistance;
    [SerializeField] private bool displayFollowRange;
    [SerializeField] private Transform target;

    [SerializeField] private NavMeshAgent agent;

    public event Action AttackRangeReached;
    public event Action AttackRangeExited;
    public event Action SteppedAway;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        /*
        var distance = Vector3.Distance(transform.position, target.position);

        if (distance <= attackRange)
        {
            AttackRangeReached?.Invoke();
        }
        else
        {
            AttackRangeExited?.Invoke();
        }*/
    }

    public void MoveToTarget()
    {
        agent.SetDestination(target.position - (target.position - transform.position).normalized * stopDistance);
    }
    
    public void StepAwayFromTarget()
    {
        Vector3 newPosition = (transform.position - target.position).normalized * stepAwayDistance;
        if(NavMesh.SamplePosition(newPosition, out NavMeshHit myNavHit, 5, -1))
        {
            agent.SetDestination(myNavHit.position);
            
            //раскомментировать для дебага
            /*
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = myNavHit.position;
            */
        }
    }

    public void StopMoving()
    {
        agent.SetDestination(transform.position);
    }

    public void OnDrawGizmosSelected()
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