public class EnemyAttackBehaviour : State
{
    private EnemyAttackStateMachine attackStateMachine;

    public EnemyAttackBehaviour(EnemyAttackStateMachine attackStateMachine)
    {
        this.attackStateMachine = attackStateMachine;
    }

    public override void Enter()
    {
        attackStateMachine.Initialize();
        attackStateMachine.SetEnemyInAttackBehaviour();
    }

    public override void Exit()
    {
        attackStateMachine.Stop();
        attackStateMachine.Reinitialize();
    }
}