public class PlayerInAttackBehaviour : State
{
    private PlayerAttack _attack;
    private PlayerDash _dash;
    private PlayerMovement _movement;
    private PlayerRotation _rotation;
    private PlayerHealth _health;

    public PlayerInAttackBehaviour(PlayerAttack attack, PlayerDash dash, PlayerMovement movement, PlayerRotation playerRotation, PlayerHealth health)
    {
        _attack = attack;
        _dash = dash;
        _movement = movement;
        _rotation = playerRotation;
        _health = health;
    }

    public override void Enter()
    {
        _attack.enabled = false;
        _dash.enabled = false;
        _health.enabled = false;
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
        _health.enabled = true;
    }
}
