using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    private PlayerInput _input;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
    }

    private void Move()
    {
        Vector3 direction = _input.GetMoveDirection();
        transform.Translate(direction * _speed * Time.deltaTime);
    }

    //DELETE METHOD
    private void Update()
    {
        Move();
    }
}
