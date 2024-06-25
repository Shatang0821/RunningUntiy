using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/CoyoteTime", fileName = "PlayerState_Coyote")]
class PlayerCoyoteTimeState : PlayerState
{
    [SerializeField] float coyoteTime = 0.1f;       //�R���e�^�C���p������
    [SerializeField] float moveSpeed;               //�ړ����x

    public override void Enter()
    {
        base.Enter();
        stateTimer = coyoteTime;
        player.SetUseGravity(0);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(stateMachine.CheckCurrentState(this)) return;
        if (input.Jump)
        {
            stateMachine.SwitchState(typeof(PlayerJumpState));
            return;
        }


        if (stateTimer < 0 && !Dash)
        {
            stateMachine.SwitchState(typeof(PlayerFallState));
            return;
        }
            
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        player.SetVelocityX(xInput * moveSpeed);
    }
}