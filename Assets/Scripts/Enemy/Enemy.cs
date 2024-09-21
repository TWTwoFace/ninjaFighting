using UnityEngine;

public class Enemy : MonoBehaviour
{
	public void Init(Transform target)
	{
		
	}

	public void Dispose()
	{
		gameObject.SetActive(false);
	}
}