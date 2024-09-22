public class EnemyHittedBehaviour : State
{
    private EnemyMovement _enemyMovement;

    public EnemyHittedBehaviour(EnemyMovement enemyMovement)
    {
        _enemyMovement = enemyMovement;
    }

    public override void Enter()
    {
        _enemyMovement.DetermineHittedDirection();
    }

    public override void Update()
    {
        _enemyMovement.HittedMove();
    }
}