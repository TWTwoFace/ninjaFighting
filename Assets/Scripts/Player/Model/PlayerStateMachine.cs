using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [SerializeField] private PlayerAttack _playerAttack;
    [SerializeField] private PlayerDash _playerDash;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerRotation _playerRotation;

    protected override void InitBehaviours()
    {
        base.InitBehaviours();
        _behavioursMap[typeof(PlayerMovementBehaviour)] = new PlayerMovementBehaviour(_playerMovement, _playerRotation);
        _behavioursMap[typeof(PlayerInDashBehaviour)] = new PlayerInDashBehaviour(_playerAttack, _playerDash, _playerMovement);
        _behavioursMap[typeof(PlayerInAttackBehaviour)] = new PlayerInAttackBehaviour(_playerAttack, _playerDash, _playerMovement, _playerRotation);
    }

    protected override void SetBehaviourByDefault()
    {
        SetPlayerMovementBehaviour();
    }

    private void SetPlayerMovementBehaviour()
    {
        var behaviour = GetBehaviour<PlayerMovementBehaviour>();
        SetBehaviour(behaviour);
    }

    private void SetPlayerInDashBehaviour()
    {
        var behaviour = GetBehaviour<PlayerInDashBehaviour>();
        SetBehaviour(behaviour);
    }

    private void SetPlayerInAttackBehaviour()
    {
        var behaviour = GetBehaviour<PlayerInAttackBehaviour>();
        SetBehaviour(behaviour);
    }

    protected override void Subscribe()
    {
        _playerAttack.Attacked += SetPlayerMovementBehaviour;
        _playerDash.Dashed += SetPlayerMovementBehaviour;
        _playerAttack.Started += SetPlayerInAttackBehaviour;
        _playerDash.Started += SetPlayerInDashBehaviour;
    }

    protected override void Unsubscribe()
    {
        _playerAttack.Attacked -= SetPlayerMovementBehaviour;
        _playerDash.Dashed -= SetPlayerMovementBehaviour;
        _playerAttack.Started -= SetPlayerInAttackBehaviour;
        _playerDash.Started -= SetPlayerInDashBehaviour;
    }
}
