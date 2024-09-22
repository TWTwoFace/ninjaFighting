using System;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    public event Action<Vector3> AttackDirectionDeterminded;
    public bool isMoving { get; private set; }

    [SerializeField] private Transform _visualsTransform;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _inAttackSpeed;
    [SerializeField] private float _dashSpeed;

    private PlayerAttack _attack;
    private PlayerInput _input;

    private Vector3 _attackDirection;

    private void Awake()
    {
        _attack = GetComponent<PlayerAttack>();
        _input = GetComponent<PlayerInput>();
        _attack.Started += GetAttackDirection;
    }

    public void Move()
    {
        Vector3 direction = _input.GetMoveDirection();

        isMoving = direction != Vector3.zero;

        transform.Translate(direction * _runSpeed * Time.deltaTime);
    }

    public void AttackMove()
    {
        transform.Translate(_attackDirection * _inAttackSpeed * Time.deltaTime);
    }

    private void GetAttackDirection()
    {
        _attackDirection = transform.position - Camera.main.transform.position;
        _attackDirection.y = 0;
        _attackDirection.Normalize();
        AttackDirectionDeterminded?.Invoke(_attackDirection);
    }

    public void DashMove()
    {
        transform.Translate(_visualsTransform.forward * _dashSpeed * Time.deltaTime);
    }

    private void OnDisable()
    {
        _attack.Started -= GetAttackDirection;
    }
}
