public class PlayerDeadBehaviour : State
{
    private PlayerAttack _attack;
    private PlayerDash _dash;
    private PlayerMovement _movement;
    private PlayerRotation _rotation;

    public PlayerDeadBehaviour(PlayerAttack attack, PlayerDash dash)
    {
        _attack = attack;
        _dash = dash;
    }

    public override void Enter()
    {
        _attack.enabled = false;
        _dash.enabled = false;
    }

}