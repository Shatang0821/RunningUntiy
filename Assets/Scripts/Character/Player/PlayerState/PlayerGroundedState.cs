using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    private float coyoteTime;�@  // �R���[�e�^�C���i�n�ʂ��痣�ꂽ��ɂ܂��W�����v�ł���P�\���ԁj
    protected int currentFrame;�@// ���݂̃t���[����

    public override void Enter()
    {
        currentFrame = 0;

        coyoteTime = 0.08f; // �R���[�e�^�C����ݒ�

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
        {
            // �R���[�e�^�C�����ł���΃W�����v�\
            if (coyoteTime > 0)
            {
                coyoteTime -= Time.deltaTime;
                if (Jump)
                    stateMachine.SwitchState(typeof(PlayerJumpState));
                return;
            }
        }

        // �ǂɐڐG���Ă��āA�o����͂�����ꍇ�A�o���Ԃɐ؂�ւ���
        if (player.IsWallDetected() && Climb)
            stateMachine.SwitchState(typeof(PlayerClimbState));

        // �n�ʂɂ��Ȃ��ꍇ�A������Ԃɐ؂�ւ���
        if (!player.IsGroundDetected())
            stateMachine.SwitchState(typeof(PlayerFallState));

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