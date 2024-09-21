using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    public bool isMoving { get; private set; }

    [SerializeField] private Transform _visualsTransform;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _inAttackSpeed;
    [SerializeField] private float _dashSpeed;

    private PlayerInput _input;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
    }

    public void Move()
    {
        Vector3 direction = _input.GetMoveDirection();

        isMoving = direction != Vector3.zero;

        transform.Translate(direction * _runSpeed * Time.deltaTime);
    }

    public void AttackMove()
    {
        transform.Translate(_visualsTransform.forward * _inAttackSpeed * Time.deltaTime);
    }

    public void DashMove()
    {
        transform.Translate(_visualsTransform.forward * _dashSpeed * Time.deltaTime);
    }
}
