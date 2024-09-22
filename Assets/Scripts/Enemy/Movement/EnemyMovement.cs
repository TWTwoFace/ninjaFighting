using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public event Action AttackRangeReached;
    public event Action SteppedAway;

    [SerializeField] private float followRange;
    [SerializeField] private float attackRange;
    [SerializeField] private float stopDistance;
    [SerializeField] private float stepAwayDistance;
    [SerializeField] private bool displayFollowRange;
    [SerializeField] private Transform target;

    [SerializeField] private NavMeshAgent agent;

    private Vector3 _stepAwayPosition;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void MoveToTarget()
    {
        agent.SetDestination(target.position - (target.position - transform.position).normalized * stopDistance);
        DetermineMovedToTarget();
    }
    
    public void StepAwayFromTarget()
    {
        Vector3 newPosition = transform.position + (transform.position - target.position).normalized * stepAwayDistance;

        print((target.position - newPosition).magnitude);

        if(NavMesh.SamplePosition(newPosition, out NavMeshHit myNavHit, 2, -1))
        {
            agent.SetDestination(myNavHit.position);
            _stepAwayPosition = myNavHit.position;
        }
    }

    public void DetermineSteppedAway()
    {
        Vector2 point = new Vector2(_stepAwayPosition.x, _stepAwayPosition.z);
        Vector2 agentPoint = new Vector2(agent.transform.position.x, agent.transform.position.z);
        if ((point - agentPoint).magnitude < 0.1f)
            SteppedAway?.Invoke();
    }

    public void StopMoving()
    {
        agent.SetDestination(transform.position);
    }

    public void RotateTowardsTarget()
    {
        var newRotation = Quaternion.LookRotation((target.position - transform.position).normalized, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, Time.deltaTime * 500f);
    }

    private void DetermineMovedToTarget()
    {
        var distance = Vector3.Distance(transform.position, target.position) - 0.1f;

        if (distance > attackRange)
            return;

        AttackRangeReached?.Invoke();
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        int rayAmount = 16;
        Gizmos.color = Color.yellow;
        for (int i = 0; i < rayAmount; i++)
        {
            var angle = Quaternion.Euler(0f, 360f/rayAmount*i + 12.25f, 0f) * agent.transform.forward;
            Gizmos.DrawLine(agent.transform.position, agent.transform.position + angle * followRange);
        }
    }
#endif
}