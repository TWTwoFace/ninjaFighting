using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    private PlayerMovement _movement;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (_movement.isMoving)
            _animator.SetFloat("Speed", 1f);
        else
            _animator.SetFloat("Speed", 0f);
    }
}
