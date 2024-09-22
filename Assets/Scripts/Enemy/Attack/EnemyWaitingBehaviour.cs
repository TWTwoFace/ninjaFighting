public class EnemyWaitingBehaviour : State
{
    private EnemyWaiting _enemyWaiting;
    private EnemyMovement _enemyMovement;

    public EnemyWaitingBehaviour(EnemyWaiting enemyWaiting, EnemyMovement enemyMovement)
    {
        _enemyWaiting = enemyWaiting;
        _enemyMovement = enemyMovement;
    }

    public override void Enter()
    {
        _enemyWaiting.WaitRandomTime();
    }

    public override void Update()
    {
        _enemyMovement.RotateTowardsTarget();
    }
    
    public override void Exit()
    {
        _enemyWaiting.StopAllCoroutines();
    }
}