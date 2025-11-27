using UnityEditor;
using UnityEngine;

public class JumpState : StateBase
{

    private bool _isFallingAnimation = false;

    public JumpState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine) { }

    public override void EnterState()
    {
        Vector2 jumpForceVec = new Vector2(_player.rb.linearVelocity.x, _player.jumpForce);
        _player.rb.linearVelocity = jumpForceVec;

        _player.animator.SetTrigger(_player.triggerJump);

        _player.animator.SetBool(_player.boolDown, false);
        _isFallingAnimation = false;

        Debug.Log("JumpState: Entrou. Disparando trigger 'jump' e bool 'down' false.");

    }

    public override void ExitState() 
    {
        _player.animator.SetBool(_player.boolDown, false);
    }
    public override void UpdateState()
    {
        if(_player.rb.linearVelocity.y < 0 && !_isFallingAnimation)
        {
            _player.animator.SetBool(_player.boolDown, true);
            _isFallingAnimation = true;
            Debug.Log("JumpState: Velocidade Y < 0. Ativando bool 'down' true.");
        }

        if (_player.PlayerGroundedCheck())
        {
            Debug.Log("JumpState: PlayerGroundedCheck() é TRUE. Saindo para IdleState.");
            _playerStateMachine.SwitchState(typeof(IdleState));
            return;
        }

        float horizontalSpeed = 0;
        float currentSpeed = Input.GetKey(_player.runInput) ? _player.runSpeed : _player.moveSpeed;
        if (Input.GetKey(_player.right))
            horizontalSpeed = currentSpeed;
        else if(Input.GetKey(_player.left))
            horizontalSpeed = -currentSpeed;

        _player.rb.linearVelocity = new Vector2(horizontalSpeed, _player.rb.linearVelocity.y);
    }
}


#region Tentativa de código sem ajuda
/*
using UnityEditor;
using UnityEngine;

public class JumpState : StateBase
{
    public JumpState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine) { }

    public override void EnterState()
    {
        _player.animator.SetTrigger(_player.triggerJump);
    }

    public override void ExitState() { }
    public override void UpdateState()
    {
        float verticalForce = 0;
        var Jump = _player.jumpForce;


        if (Input.GetKeyDown(_player.jumpInput) && _player.PlayerGroundedCheck())
        {
            verticalForce = Jump;
        }

        else if (_player.rb.linearVelocity.y < 0)
        {
            _player.animator.SetTrigger(_player.triggerDown);
        }

        else
            _playerStateMachine.SwitchState(typeof(IdleState));

        _player.rb.linearVelocity = new Vector2(_player.rb.linearVelocity.x, verticalForce);
    }
}
*/

#endregion
