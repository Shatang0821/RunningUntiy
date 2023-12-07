using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected float coyoteTime;
    protected int currentFrame;

    public override void Enter()
    {
        currentFrame = 0;
        coyoteTime = .3f;
        base.Enter();
        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(Jump)
            stateMachine.SwitchState(typeof(PlayerJumpState));
        if (!player.IsGroundDetected() && !Jump)
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
            player.SetVelocityX(targetSpeed);
        }
    }
}