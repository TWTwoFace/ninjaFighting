using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private Transform _visualsTransform;

    private const float _rotationSpeed = 500f;
    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    public void RotateToMoveDirection()
    {
        var direction = _playerInput.GetMoveDirection();

        if (direction == Vector3.zero)
            return;

        Quaternion rotation = Quaternion.LookRotation(direction);
        _visualsTransform.rotation = Quaternion.RotateTowards(_visualsTransform.rotation, rotation, _rotationSpeed * Time.deltaTime);
    }
}
