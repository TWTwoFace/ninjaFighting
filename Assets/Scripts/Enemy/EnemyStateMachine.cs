using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateMachine
{
    [SerializeField] private EnemyMovement enemyMovement;
    [SerializeField] private EnemyDeath enemyDeath;
    [SerializeField] private EnemyAttackStateMachine attackStateMachine;
    [SerializeField] private EnemyHealth enemyHealth;
    [SerializeField] private NavMeshAgent navMeshAgent;

    protected override void InitBehaviours()
    {
        base.InitBehaviours();
        _behavioursMap[typeof(EnemyAttackBehaviour)] = new EnemyAttackBehaviour(attackStateMachine);
        _behavioursMap[typeof(EnemyMovementBehaviour)] = new EnemyMovementBehaviour(enemyMovement);
        _behavioursMap[typeof(EnemyDeathBehaviour)] = new EnemyDeathBehaviour(enemyDeath, enemyMovement, navMeshAgent);
        _behavioursMap[typeof(EnemyHittedBehaviour)] = new EnemyHittedBehaviour(enemyMovement);
    }

    protected override void SetBehaviourByDefault()
    {
        SetEnemyMovementBehaviour();
    }

    private void SetEnemyMovementBehaviour()
    {
        var behaviour = GetBehaviour<EnemyMovementBehaviour>();
        SetBehaviour(behaviour);
    }
    
    private void SetEnemyAttackBehaviour()
    {
        var behaviour = GetBehaviour<EnemyAttackBehaviour>();
        SetBehaviour(behaviour);
    }
    
    private void SetEnemyDeathBehaviour()
    {
        var behaviour = GetBehaviour<EnemyDeathBehaviour>();
        SetBehaviour(behaviour);
    }

    private void SetEnemyHittedBehaviour()
    {
        var behaviour = GetBehaviour<EnemyHittedBehaviour>();
        SetBehaviour(behaviour);
    }

    protected override void Subscribe()
    {
        enemyMovement.AttackRangeReached += SetEnemyAttackBehaviour;
        attackStateMachine.Ended += SetEnemyMovementBehaviour;
        enemyHealth.Damaged += SetEnemyHittedBehaviour;
        enemyHealth.Dead += SetEnemyDeathBehaviour;
    }

    protected override void Unsubscribe()
    {
        enemyMovement.AttackRangeReached -= SetEnemyAttackBehaviour;
        attackStateMachine.Ended -= SetEnemyMovementBehaviour;
        enemyHealth.Damaged -= SetEnemyHittedBehaviour;
        enemyHealth.Dead -= SetEnemyDeathBehaviour;
    }
}