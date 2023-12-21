using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected float coyoteTime;
    protected int currentFrame;

    public override void Enter()
    {
        currentFrame = 0;
        coyoteTime = 0.1f;
        //�n�ʂɖ߂�ƃ_�b�V�����Z�b�g
        dashTrigger = false;
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.FlipController(xInput);

        if (Jump)
            stateMachine.SwitchState(typeof(PlayerJumpState));

        if (!player.IsGroundDetected())
        {
            if(coyoteTime > 0)
            {
                coyoteTime -= Time.deltaTime;
                if (Jump)
                    stateMachine.SwitchState(typeof(PlayerJumpState));
                return;
            }
        }
        if (player.IsWallDetected() && Climb)
            stateMachine.SwitchState(typeof(PlayerClimbState));
            
        if (!player.IsGroundDetected())
            stateMachine.SwitchState(typeof(PlayerFallState));

    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        currentFrame++;
    }
        
    /// <summary>
    /// �t���[���ő��x�R���g���[��
    /// </summary>
    /// <param name="targetSpeed">�ڕW���x</param>
    /// <param name="currentFrame">���ݑ��x</param>
    /// <param name="totalFrames">�ō����x�̃t���[��</param>
    protected void ChangeVelocity(float targetSpeed, int currentFrame, int totalFrames)
    {
        if (currentFrame < totalFrames)
        {
            float lerpFactor = (float)currentFrame / totalFrames;
            float currentVelocityX = Mathf.Lerp(rb.velocity.x, targetSpeed, lerpFactor);
            player.SetVelocityX(currentVelocityX);
        }
        else
        {
            player.SetVelocityX(targetSpeed );
        }
    }
}