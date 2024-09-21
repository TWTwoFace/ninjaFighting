public class EnemyWaitingBehaviour : State
{
    private EnemyWaiting _enemyWaiting;

    public EnemyWaitingBehaviour(EnemyWaiting enemyWaiting)
    {
        _enemyWaiting = enemyWaiting;
    }

    public override void Enter()
    {
        _enemyWaiting.WaitRandomTime();
    }

    public override void Exit()
    {
        _enemyWaiting.StopAllCoroutines();
    }
}