using System;
using UnityEngine;

public class EnemyAttackStateMachine : StateMachine, INestedStateMachine
{
    public event Action Ended;

    [SerializeField] private EnemyAttack enemyAttack;
    [SerializeField] private EnemyMovement enemyMovement;
    [SerializeField] private EnemyWaiting enemyWaiting;    

    public void Reinitialize()
    {
        SetBehaviourByDefault();
    }

    public void Initialize()
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
        _behavioursMap[typeof(EnemyAttackIdleBehaviour)] = new EnemyAttackIdleBehaviour();
        _behavioursMap[typeof(EnemyInAttackBehaviour)] = new EnemyInAttackBehaviour(enemyAttack);
        _behavioursMap[typeof(EnemyStepAwayBehaviour)] = new EnemyStepAwayBehaviour(enemyMovement);
        _behavioursMap[typeof(EnemyWaitingBehaviour)] = new EnemyWaitingBehaviour(enemyWaiting, enemyMovement);
        Stop();
    }

    protected override void SetBehaviourByDefault()
    {
        SetEnemyAttackIdleBehaviour();
    }

    private void SetEnemyAttackIdleBehaviour()
    {
        var behaviour = GetBehaviour<EnemyAttackIdleBehaviour>();
        SetBehaviour(behaviour);
    }

    public void SetEnemyInAttackBehaviour()
    {
        var behaviour = GetBehaviour<EnemyInAttackBehaviour>();
        SetBehaviour(behaviour);
    }

    private void SetEnemyStepAwayBehaviour()
    {
        var behaviour = GetBehaviour<EnemyStepAwayBehaviour>();
        SetBehaviour(behaviour);
    }

    private void SetEnemyWaitingBehaviour()
    {
        var behaviour = GetBehaviour<EnemyWaitingBehaviour>();
        SetBehaviour(behaviour);
    }

    private void OnCycleEnd()
    {
        Ended?.Invoke();
    }

    protected override void Subscribe()
    {
        enemyAttack.AttackPerformed += SetEnemyStepAwayBehaviour;
        enemyMovement.SteppedAway += SetEnemyWaitingBehaviour;
        enemyWaiting.Ended += OnCycleEnd;
    }

    protected override void Unsubscribe()
    {
        enemyAttack.AttackPerformed -= SetEnemyStepAwayBehaviour;
        enemyMovement.SteppedAway -= SetEnemyWaitingBehaviour;
        enemyWaiting.Ended -= OnCycleEnd;
    }
}