public class PlayerMovementBehaviour : State
{
    private PlayerMovement _movement;
    private PlayerRotation _rotation;

    public PlayerMovementBehaviour(PlayerMovement movement, PlayerRotation rotation)
    {
        _movement = movement;
        _rotation = rotation;
    }

    public override void Update()
    {
        _movement.Move();
        _rotation.RotateToMoveDirection();
    }
}
