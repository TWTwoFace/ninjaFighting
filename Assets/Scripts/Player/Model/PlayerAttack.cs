using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerInput), typeof(Animator))]
public class PlayerAttack : MonoBehaviour
{
    public event Action Started;
    public event Action Attacked;

    private const string _triggerName = "Attack";

    private PlayerInput _input;
    private Animator _animator;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _animator = GetComponent<Animator>();
    }

    public void OnAttackEnd()
    {
        Attacked?.Invoke();
    }

    private void Attack()
    {
        if (enabled == false)
            return;

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

}
