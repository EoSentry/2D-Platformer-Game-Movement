using DG.Tweening;
using UnityEngine;

public class WalkState : StateBase
{
    public WalkState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine) { }

    public override void EnterState()
    {
        _player.animator.SetBool(_player.boolRun, true);
    }
    public override void ExitState() 
    {
        _player.animator.SetBool(_player.boolRun, false);
    }
    public override void UpdateState()
    {
        if(Input.GetKeyDown(_player.jumpInput) && _player.PlayerGroundedCheck())
        {
            _playerStateMachine.SwitchState(typeof(JumpState));
            return;
        }

        float currentSpeed = Input.GetKey(_player.runInput) ? _player.runSpeed : _player.moveSpeed;
        float horizontalSpeed = 0;
        
        if(Input.GetKey(_player.right))
        {
            horizontalSpeed = currentSpeed;

            if(_player.rb.transform.localScale.x != 1)
            {
                _player.transform.DOScaleX(1, _player.timeBetweenRotation);
            }
        }

        else if(Input.GetKey(_player.left))
        {
            horizontalSpeed = -currentSpeed;

            if(_player.rb.transform.localScale.x != -1)
            {
                _player.transform.DOScaleX(-1, _player.timeBetweenRotation);
            }
        }

        else
        {
            _playerStateMachine.SwitchState(typeof(IdleState));
            return;
        }

        _player.rb.linearVelocity = new Vector2(horizontalSpeed, _player.rb.linearVelocity.y);
    }
}
