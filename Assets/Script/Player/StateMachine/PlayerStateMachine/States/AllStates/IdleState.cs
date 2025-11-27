using UnityEngine;

public class IdleState : StateBase
{
    public IdleState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine) { }



    public override void EnterState()
    {
        _player.animator.SetBool(_player.boolRun, false);
        _player.rb.linearVelocity = new Vector2(0, _player.rb.linearVelocity.y);
    }
    public override void ExitState() { }
    public override void UpdateState() 
    {
        if(Input.GetKeyDown(_player.jumpInput) && _player.PlayerGroundedCheck())
        {
            _playerStateMachine.SwitchState(typeof(JumpState));
            return;
        }


        if (Input.GetKey(_player.right) || Input.GetKey(_player.left))
        {
            _playerStateMachine.SwitchState(typeof(WalkState));
        }
    }
}
