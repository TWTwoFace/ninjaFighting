using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerCameraRotation : MonoBehaviour
{
    [SerializeField] private Transform _cameraPointTransform;

    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    private void RotateCamera()
    {
        /*Vector2 delta = _playerInput.GetMouseDelta();
        print(delta);
        _cameraPointTransform.rotation = Quaternion.Euler(delta.x, delta.y, 0f);*/
    }

    private void Update()
    {
        RotateCamera();
    }

}
