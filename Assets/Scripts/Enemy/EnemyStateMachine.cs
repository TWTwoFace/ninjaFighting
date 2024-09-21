using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    [SerializeField] private EnemyAttack enemyAttack;
    [SerializeField] private EnemyMovement enemyMovement;

    protected override void InitBehaviours()
    {
        base.InitBehaviours();
        //_behavioursMap[typeof(EnemyAttackBehaviour)] = new EnemyAttackBehaviour();
        _behavioursMap[typeof(EnemyMovementBehaviour)] = new EnemyMovementBehaviour(enemyMovement);
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

    protected override void Subscribe()
    {
        enemyMovement.AttackRangeReached += SetEnemyAttackBehaviour;
        enemyMovement.AttackRangeExited += SetEnemyMovementBehaviour;
        enemyAttack.AttackPerformed += SetEnemyMovementBehaviour;
    }

    protected override void Unsubscribe()
    {
        enemyMovement.AttackRangeReached -= SetEnemyAttackBehaviour;
        enemyMovement.AttackRangeExited -= SetEnemyMovementBehaviour;
        enemyAttack.AttackPerformed -= SetEnemyMovementBehaviour;
    }
}