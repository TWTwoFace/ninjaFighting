public class EnemyInAttackBehaviour : State
{
	private EnemyAttack enemyAttack;

	public EnemyInAttackBehaviour(EnemyAttack enemyAttack)
	{
		this.enemyAttack = enemyAttack;
	}

	public override void Enter()
	{
		enemyAttack.Attack();
	}
}