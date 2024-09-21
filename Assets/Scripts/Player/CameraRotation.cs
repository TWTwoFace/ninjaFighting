using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private Transform shoulderPoint;
    [SerializeField] private Transform _standartPositionPoint;
    [SerializeField] private Transform _visualsTransform;

    [SerializeField] private float _timeToReturnCamera;

    [SerializeField] private float verticalRotationPower;
    [SerializeField] private float horizontalRotationPower;

    [SerializeField] private float maxVerticalAngle;
    [SerializeField] private float minVerticalAngle;
    
    private PlayerInput playerInput;
    private PlayerMovement playerMovement;

    private Vector3 defaultStandartPosition;

    private float _timer = 0f;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        defaultStandartPosition = _standartPositionPoint.localPosition;
        Quaternion standartRotation = Quaternion.LookRotation(shoulderPoint.position - _standartPositionPoint.position);
        shoulderPoint.rotation = standartRotation;
    }

    private void Update()
    {
        RotateCameraByInput();
        SetStandartPositionPoint();
        TryReturnCameraToStandartPosition();
    }

    private Vector2 GetRotationDelta()
    {
        Vector2 rotationDelta = playerInput.GetMouseDelta();

        if (rotationDelta != Vector2.zero)
            _timer = 0f;

        return rotationDelta;
    }

    private void RotateCameraByInput()
    {
        Vector2 rotationDelta = GetRotationDelta();

        shoulderPoint.rotation *= Quaternion.AngleAxis(rotationDelta.x * horizontalRotationPower * Time.deltaTime, Vector3.up);
        shoulderPoint.rotation *= Quaternion.AngleAxis(rotationDelta.y * horizontalRotationPower * Time.deltaTime, Vector3.right);
        var angles = shoulderPoint.localEulerAngles;
        angles.z = 0f;
        if (angles.x > 0 && angles.x < 180f) angles.x = Mathf.Min(angles.x, maxVerticalAngle);
        else if (angles.x < 360f && angles.x > 180f) angles.x = Mathf.Max(angles.x, 360f + minVerticalAngle);
        shoulderPoint.localEulerAngles = angles;
    }

    private void ReturnCameraToStandartPosition()
    {
        Quaternion standartRotation = Quaternion.LookRotation(shoulderPoint.position - _standartPositionPoint.position);

        shoulderPoint.rotation = Quaternion.Slerp(shoulderPoint.rotation, standartRotation, 1.5f * Time.deltaTime);
    }

    private void TryReturnCameraToStandartPosition()
    {
        if (_timer > _timeToReturnCamera && playerMovement.isMoving)
        {
            ReturnCameraToStandartPosition();
        }

        _timer += Time.deltaTime;
    }

    private void SetStandartPositionPoint()
    {
        Vector3 pos = Camera.main.transform.position - _visualsTransform.position;
        pos.y = 0;
        
        if (pos.normalized == _visualsTransform.forward)
        {
            _standartPositionPoint.localPosition = new Vector3(-defaultStandartPosition.x, defaultStandartPosition.y, -defaultStandartPosition.z);
        }
        else
        {
            _standartPositionPoint.localPosition = defaultStandartPosition;
        }
    }
}
