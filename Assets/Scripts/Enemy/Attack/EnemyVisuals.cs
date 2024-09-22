using UnityEngine;

public class EnemyVisuals : MonoBehaviour
{
	[SerializeField] private float moveDeltaAnimationThreshold;
	[SerializeField] private EnemyHealth _health;

	private Animator animator;
	
	private Vector3 previousPosition;

	private const string Speed = "Speed";

	private float blendSpeed = 1f;
	
	private void Awake()
	{
		previousPosition = transform.position;
		animator = GetComponent<Animator>();
	}

	private void Update()
	{
		var currentSpeed = animator.GetFloat(Speed);
		if (previousPosition != transform.position)
		{
			if ((previousPosition - transform.position).magnitude >= moveDeltaAnimationThreshold)
			{
				var newSpeed = Mathf.Lerp(currentSpeed, 1f, Time.deltaTime * blendSpeed * 10f);
				animator.SetFloat(Speed, newSpeed);
			}
			previousPosition = transform.position;
		}
		else
		{
			var newSpeed = Mathf.Lerp(currentSpeed, 0f, Time.deltaTime * blendSpeed * 2f);
			animator.SetFloat(Speed, newSpeed);
		}
	}

	private void OnDamaged()
	{
		animator.SetTrigger("Hitted");
	}

	private void OnEnable()
	{
		_health.Damaged += OnDamaged;
	}

	private void OnDisable()
	{
		_health.Damaged -= OnDamaged;
	}
}