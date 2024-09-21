using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    private PlayerMovement _movement;
    private Animator _animator;

    private const string Speed = "Speed";
    float blendSpeed = 1f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<PlayerMovement>();
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
}
