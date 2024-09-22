using System.Collections;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
	[SerializeField] private float timeBeforeDisappear;
	
	private Animator animator;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	public void PerformDeath()
	{
		StartCoroutine(DeathRoutine());
	}

	private IEnumerator DeathRoutine()
	{
		animator.SetTrigger("Death");
		yield return new WaitForSeconds(timeBeforeDisappear);
		float time = 2f;
		while (time > 0f)
		{
			transform.Translate(Vector3.down * Time.deltaTime);
			
			time -= Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
	}
}