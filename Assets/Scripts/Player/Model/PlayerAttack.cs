using System;
using UnityEngine;

[RequireComponent(typeof(PlayerInput), typeof(Animator))]
public class PlayerAttack : MonoBehaviour
{
    public event Action Started;
    public event Action Attacked;

    [SerializeField] private Collider _weaponCollider;
    [SerializeField, Min(0)] private int _damage;

    private const string _triggerName = "Attack";

    private PlayerInput _input;
    private Animator _animator;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _weaponCollider.enabled = false;
    }
    
    public void OnAttackEnd()
    {
        Attacked?.Invoke();
        _weaponCollider.enabled = false;
    }

    private void Attack()
    {
        if (enabled == false)
            return;

        _weaponCollider.enabled = true;

        Started?.Invoke();
        _animator.SetTrigger(_triggerName);
    }

    private void OnEnable()
    {
        _input.Attacked += Attack;
    }

    private void OnDisable()
    {
        _input.Attacked -= Attack;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<EnemyHealth>(out var enemyHealth))
        {
            enemyHealth.TakeDamage(_damage);
        }
    }
}
