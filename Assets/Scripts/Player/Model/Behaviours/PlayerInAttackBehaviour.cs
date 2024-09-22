public class PlayerInAttackBehaviour : State
{
    private PlayerAttack _attack;
    private PlayerDash _dash;
    private PlayerMovement _movement;
    private PlayerRotation _rotation;

    public PlayerInAttackBehaviour(PlayerAttack attack, PlayerDash dash, PlayerMovement movement, PlayerRotation playerRotation)
    {
        _attack = attack;
        _dash = dash;
        _movement = movement;
        _rotation = playerRotation;
    }

    public override void Enter()
    {
        _attack.enabled = false;
        _dash.enabled = false;
    }

    public override void Update()
    {
        _movement.AttackMove();
        _rotation.RotateToAttackDirection();
    }

    public override void Exit()
    {
        _attack.enabled = true;
        _dash.enabled = true;
    }
}
