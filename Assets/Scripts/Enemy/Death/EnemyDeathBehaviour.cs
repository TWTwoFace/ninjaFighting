using UnityEngine.AI;

public class EnemyDeathBehaviour : State
{
	private EnemyMovement enemyMovement;
	private EnemyDeath enemyDeath;
	private NavMeshAgent navMeshAgent;

	public EnemyDeathBehaviour(EnemyDeath enemyDeath, EnemyMovement enemyMovement, NavMeshAgent navMeshAgent)
	{
		this.enemyDeath = enemyDeath;
		this.enemyMovement = enemyMovement;
		this.navMeshAgent = navMeshAgent;
	}

	public override void Enter()
	{
		enemyMovement.enabled = false;
		navMeshAgent.enabled = false;
		enemyDeath.PerformDeath();
	}
}