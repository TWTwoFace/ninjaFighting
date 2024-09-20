using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public event Action Attacked;
    public event Action Dashed;

    private PlayerInputActions _input;
    private Camera _camera;

    private void Awake()
    {
        _input = new PlayerInputActions();
        _input.Player.Enable();
        _camera = Camera.main;
    }

    public Vector3 GetMoveDirection()
    {
        Vector3 cameraForwardProjection = new Vector3(_camera.transform.forward.x, 0f, _camera.transform.forward.z).normalized;
        Vector3 cameraRightProjection = new Vector3(_camera.transform.right.x, 0f, _camera.transform.right.z).normalized;
        Vector2 rawInputDirection = _input.Player.Move.ReadValue<Vector2>();
        Vector3 direction = cameraForwardProjection * rawInputDirection.y + cameraRightProjection * rawInputDirection.x;
        return direction.normalized;
    }

    public Vector2 GetMouseDelta()
    {
        Vector2 delta = _input.Player.CameraRotation.ReadValue<Vector2>();
        return delta;
    }

    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        Attacked?.Invoke();
    }

    private void OnDashPerformed(InputAction.CallbackContext context)
    {
        Dashed?.Invoke();
    }

    private void OnEnable()
    {
        _input.Player.Attack.performed += OnAttackPerformed;
        _input.Player.Dash.performed += OnDashPerformed;
    }

    private void OnDisable()
    {
        _input.Player.Attack.performed -= OnAttackPerformed;
        _input.Player.Dash.performed -= OnDashPerformed;
    }
}
