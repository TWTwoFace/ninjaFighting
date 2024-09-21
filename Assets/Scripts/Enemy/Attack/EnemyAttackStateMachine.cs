using UnityEngine;

public class EnemyAttackStateMachine : StateMachine, INestedStateMachine
{
    [SerializeField] private EnemyAttack enemyAttack;
    [SerializeField] private EnemyMovement enemyMovement;
    
    public void Reset()
    {
        SetBehaviourByDefault();
    }

    public void Start()
    {
        enabled = true;
    }

    public void Stop()
    {
        enabled = false;
    }

    protected override void InitBehaviours()
    {
        base.InitBehaviours();
        _behavioursMap[typeof(EnemyInAttackBehaviour)] = new EnemyInAttackBehaviour(enemyAttack);
        _behavioursMap[typeof(EnemyStepAwayBehaviour)] = new EnemyStepAwayBehaviour(enemyMovement);
        
        Stop();
    }

    protected override void SetBehaviourByDefault()
    {
        SetEnemyInAttackBehaviour();
    }
    
    private void SetEnemyInAttackBehaviour()
    {
        var behaviour = GetBehaviour<EnemyInAttackBehaviour>();
        SetBehaviour(behaviour);
    }

    private void SetEnemyStepAwayBehaviour()
    {
        var behaviour = GetBehaviour<EnemyStepAwayBehaviour>();
        SetBehaviour(behaviour);
    }

    protected override void Subscribe()
    {
        enemyAttack.AttackPerformed += SetEnemyStepAwayBehaviour;
        enemyMovement.SteppedAway += SetEnemyInAttackBehaviour;
    }

    protected override void Unsubscribe()
    {
        enemyAttack.AttackPerformed -= SetEnemyStepAwayBehaviour;
        enemyMovement.SteppedAway += SetEnemyInAttackBehaviour;
    }

}