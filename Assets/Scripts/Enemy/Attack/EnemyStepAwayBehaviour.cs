public class EnemyStepAwayBehaviour : State
{
	private EnemyMovement enemyMovement;

	public EnemyStepAwayBehaviour(EnemyMovement enemyMovement)
	{
		this.enemyMovement = enemyMovement;
	}

	public override void Enter()
	{
		enemyMovement.StepAwayFromTarget();
	}
}