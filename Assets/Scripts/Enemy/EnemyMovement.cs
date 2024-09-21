using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float followRange;
    [SerializeField] private float attackRange;
    [SerializeField] private float stopDistance;
    [SerializeField] private bool displayFollowRange;
    [SerializeField] private Transform target;

    private NavMeshAgent agent;

    public event Action AttackRangeReached;
    public event Action AttackRangeExited;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    
    private void Update()
    {
        var distance = Vector3.Distance(transform.position, target.position);
        if (distance <= stopDistance)
        {
            StopMoving();
        }
        if (distance <= attackRange)
        {
            AttackRangeReached?.Invoke();
        }
        else
        {
            AttackRangeExited?.Invoke();
        }
    }

    public void MoveToTarget()
    {
        agent.SetDestination(target.position);
    }

    public void StopMoving()
    {
        agent.SetDestination(target.position);
    }
}