public class EnemyAttackBehaviour : State
{
    private EnemyAttackStateMachine attackStateMachine;

    public EnemyAttackBehaviour(EnemyAttackStateMachine attackStateMachine)
    {
        this.attackStateMachine = attackStateMachine;
    }

    public override void Enter()
    {
        attackStateMachine.Start();
    }

    public override void Exit()
    {
        attackStateMachine.Stop();
        attackStateMachine.Reset();
    }
}