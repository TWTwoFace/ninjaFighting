public class EnemyMovementBehaviour : State
{
	private EnemyMovement enemyMovement;

	public EnemyMovementBehaviour(EnemyMovement enemyMovement)
	{
		this.enemyMovement = enemyMovement;
	}

	public override void Update()
	{
		enemyMovement.MoveToTarget();
	}
}