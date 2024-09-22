using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    [SerializeField] private TrailRenderer katanaTrail;
    [SerializeField] private ParticleSystem leftFootParticles;
    [SerializeField] private ParticleSystem rightFootParticles;
    
    private PlayerMovement _movement;
    private PlayerAttack _attack;
    private PlayerHealth _health;

    private Animator _animator;

    private const string Speed = "Speed";
    float blendSpeed = 1f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<PlayerMovement>();
        _attack = GetComponent<PlayerAttack>();
        _health = GetComponent<PlayerHealth>();
    }

    private void Start()
    {
        _attack.Started += EmmitTrail;
        _attack.Attacked += StopEmmitTrail;
        _health.Dead += OnDead;
    }
    
    private void Update()
    {
        var currentSpeed = _animator.GetFloat(Speed);
        if (_movement.isMoving)
        {
            var newSpeed = Mathf.Lerp(currentSpeed, 1f, Time.deltaTime * blendSpeed * 5);
            _animator.SetFloat(Speed, newSpeed);
        }
        else
        {
            var newSpeed = Mathf.Lerp(currentSpeed, 0f, Time.deltaTime * blendSpeed * 5f);
            _animator.SetFloat(Speed, newSpeed);
        }
    }

    private void OnDead()
    {
        _animator.SetTrigger("Dead");
    }

    public void EmmitLeftFootstep()
    {
        leftFootParticles.Play();
    }
    
    public void EmmitRightFootstep()
    {
        rightFootParticles.Play();
    }
    
    private void EmmitTrail()
    {
        katanaTrail.emitting = true;
    }

    private void StopEmmitTrail()
    {
        katanaTrail.emitting = false;
    }
}
