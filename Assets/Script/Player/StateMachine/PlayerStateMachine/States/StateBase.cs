using UnityEngine;

public abstract class StateBase
{
    protected Player _player;
    protected PlayerStateMachine _playerStateMachine;

    protected StateBase(Player player, PlayerStateMachine playerStateMachine) 
    {
        _player = player;
        _playerStateMachine = playerStateMachine;
    }

    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void UpdateState();
}
