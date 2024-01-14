using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int currentFrame;�@// ���݂̃t���[����

    public override void Enter()
    {
        currentFrame = 0;

        // �_�b�V���g���K�[�����Z�b�g
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

        // ���͂Ɋ�Â��ăv���C���[�̌����𐧌�
        player.FlipController(xInput);

        // �W�����v���͂�����ꍇ�A�W�����v��Ԃɐ؂�ւ���
        if (Jump)
            stateMachine.SwitchState(typeof(PlayerJumpState));

        // �n�ʂ����o���Ă��Ȃ��ꍇ�̏���
        if (!player.IsGroundDetected())
            stateMachine.SwitchState(typeof(PlayerCoyoteTimeState));

        // �ǂɐڐG���Ă��āA�o����͂�����ꍇ�A�o���Ԃɐ؂�ւ���
        if (player.IsWallDetected() && Climb)
            stateMachine.SwitchState(typeof(PlayerClimbState));
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        currentFrame++; // ���݂̃t���[�������X�V
    }

    /// <summary>
    /// �t���[���P�ʂő��x�𐧌䂷�郁�\�b�h
    /// </summary>
    /// <param name="targetSpeed">�ڕW���x</param>
    /// <param name="currentFrame">���݂̃t���[����</param>
    /// <param name="totalFrames">�ō����x�ɒB����܂ł̃t���[����</param>
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