using UnityEngine;
using UnityEngine.AI;

public class EnemyAgent: MonoBehaviour
{
    private NavMeshAgent agent;
    
    public Vector3 GetLookDirection() => transform.forward;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 1f;
    }

    public void Move(Vector3 position)
    {
        agent.SetDestination(position);
    }

    public void StopMoving()
    {
        agent.SetDestination(transform.position);
    }
}
