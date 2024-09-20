using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private Transform shoulderPoint;

    [SerializeField] private float verticalRotationPower;
    [SerializeField] private float horizontalRotationPower;

    [SerializeField] private float maxVerticalAngle;
    [SerializeField] private float minVerticalAngle;
    
    private PlayerInput playerInput;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        Vector2 rotationDelta = playerInput.GetMouseDelta();

        shoulderPoint.rotation *= Quaternion.AngleAxis(rotationDelta.x * horizontalRotationPower * Time.deltaTime, Vector3.up);
        shoulderPoint.rotation *= Quaternion.AngleAxis(rotationDelta.y * horizontalRotationPower * Time.deltaTime, Vector3.right);
        var angles = shoulderPoint.localEulerAngles;
        angles.z = 0f;
        if (angles.x > 0 && angles.x < 180f) angles.x = Mathf.Min(angles.x, maxVerticalAngle);
        else if (angles.x < 360f && angles.x > 180f) angles.x = Mathf.Max(angles.x, 360f + minVerticalAngle);
        shoulderPoint.localEulerAngles = angles;
    }
}
