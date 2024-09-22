using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyVisuals : MonoBehaviour
{
	[SerializeField] private float moveDeltaAnimationThreshold;
	[SerializeField] private EnemyHealth _health;
	
	[SerializeField] private Transform hitParticlesPoint;
	[SerializeField] private ParticleSystem[] hitParticles;
	
	[SerializeField] private ParticleSystem leftFootParticles;
	[SerializeField] private ParticleSystem rightFootParticles;
	
	[SerializeField] private ParticleSystem spawnEffect;

	private Animator animator;
	
	private Vector3 previousPosition;

	private const string Speed = "Speed";

	private float blendSpeed = 1f;
	
	private void Awake()
	{
		previousPosition = transform.position;
		animator = GetComponent<Animator>();
	}

	private void Start()
	{
		spawnEffect.Play();
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
	
	public void EmmitLeftFootstep()
	{
		leftFootParticles.Play();
	}
    
	public void EmmitRightFootstep()
	{
		rightFootParticles.Play();
	}
	
	private void SpawnHitParticle()
	{
		Instantiate(hitParticles[Random.Range(0, hitParticles.Length)], hitParticlesPoint.position + transform.right * 0.1f, Quaternion.Euler(transform.right));
		Instantiate(hitParticles[Random.Range(0, hitParticles.Length)], hitParticlesPoint.position, Quaternion.identity);
		Instantiate(hitParticles[Random.Range(0, hitParticles.Length)], hitParticlesPoint.position + transform.right * -0.1f, Quaternion.Euler(transform.right));
	}

	private void OnDamaged()
	{
		animator.SetTrigger("Hitted");
		SpawnHitParticle();
	}
	
	private void OnDead()
	{
		SpawnHitParticle();
	}

	private void OnEnable()
	{
		_health.Damaged += OnDamaged;
		_health.Dead += OnDead;
	}

	private void OnDisable()
	{
		_health.Damaged -= OnDamaged;
		_health.Dead -= OnDead;
	}
}