﻿public class PlayerInDashBehaviour : State
{
    private PlayerAttack _attack;
    private PlayerDash _dash;
    private PlayerMovement _movement;

    public PlayerInDashBehaviour(PlayerAttack attack, PlayerDash dash, PlayerMovement movement)
    {
        _attack = attack;
        _dash = dash;
        _movement = movement;
    }

    public override void Enter()
    {
        _attack.enabled = false;
        _dash.enabled = false;
    }

    public override void Update()
    {
        _movement.DashMove();
    }

    public override void Exit()
    {
        _attack.enabled = true;
        _dash.enabled = true;
    }
}
