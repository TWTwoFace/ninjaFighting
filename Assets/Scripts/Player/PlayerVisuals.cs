using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    [SerializeField] private TrailRenderer katanaTrail;
    [SerializeField] private ParticleSystem leftFootParticles;
    [SerializeField] private ParticleSystem rightFootParticles;
    
    private PlayerMovement _movement;
    private PlayerAttack _attack;
    
    private Animator _animator;

    private const string Speed = "Speed";
    float blendSpeed = 1f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<PlayerMovement>();
        _attack = GetComponent<PlayerAttack>();
    }

    private void Start()
    {
        _attack.Started += EmmitTrail;
        _attack.Attacked += StopEmmitTrail;
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
            var newSpeed = Mathf.Lerp(currentSpeed, 0f, Time.deltaTime * blendSpeed * 2f);
            _animator.SetFloat(Speed, newSpeed);
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
    
    private void EmmitTrail()
    {
        katanaTrail.emitting = true;
    }

    private void StopEmmitTrail()
    {
        katanaTrail.emitting = false;
    }
}
