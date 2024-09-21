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

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StepAwayFromTarget();
        }
        MoveToTarget();
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

    private Vector3 newPos;
    public void StepAwayFromTarget()
    {
        Vector3 newPosition = Vector3.zero; 
        if (newPos == Vector3.zero)
        {
            newPosition = (transform.position - target.position).normalized * stepAwayDistance;
            if(NavMesh.SamplePosition(transform.position, out NavMeshHit myNavHit, 100 , -1))
            {
                newPosition = myNavHit.position;
                newPos = newPosition;
            }
        }
        
        if (newPosition != null) agent.SetDestination(newPosition);
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