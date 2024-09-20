using System;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public event Action Started;
    public event Action Dashed;

    private const string _triggerName = "Dash";

    private PlayerInput _input;
    private Animator _animator;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _animator = GetComponent<Animator>();
    }

    public void OnDashEnd()
    {
        Dashed?.Invoke();
    }

    private void Dash()
    {
        if (enabled == false)
            return;

        Started?.Invoke();
        _animator.SetTrigger(_triggerName);
    }

    private void OnEnable()
    {
        _input.Dashed += Dash;
    }


    private void OnDisable()
    {
        _input.Dashed -= Dash;
    }
}
