using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private Transform _visualsTransform;

    private const float _rotationSpeed = 500f;
    private PlayerInput _playerInput;
    private PlayerMovement _playerMovement;

    private Vector3 _attackDirection;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    public void RotateToMoveDirection()
    {
        var direction = _playerInput.GetMoveDirection();

        if (direction == Vector3.zero)
            return;

        Quaternion rotation = Quaternion.LookRotation(direction);
        _visualsTransform.rotation = Quaternion.RotateTowards(_visualsTransform.rotation, rotation, _rotationSpeed * Time.deltaTime);
    }

    public void RotateToAttackDirection()
    {
        Quaternion rotation = Quaternion.LookRotation(_attackDirection);
        _visualsTransform.rotation = Quaternion.RotateTowards(_visualsTransform.rotation, rotation, _rotationSpeed * Time.deltaTime);
    }
    
    private void OnAttackDirectionDeterminded(Vector3 direction)
    {
        _attackDirection = direction;
    }

    private void OnEnable()
    {
        _playerMovement.AttackDirectionDeterminded += OnAttackDirectionDeterminded;
    }

    private void OnDisable()
    {
        _playerMovement.AttackDirectionDeterminded -= OnAttackDirectionDeterminded;
    }
}
