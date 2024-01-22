using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/WallSlide", fileName = "PlayerState_WallSlide")]
public class PlayerWallSlideState : PlayerState
{
    [Header("WallSlide info")]
    [SerializeField] float gravity; // �Ǌ��莞�̏d��
    public override void Enter()
    {
        
        base.Enter();

        // �ǂɐG�ꂽ���̏d�͂�ݒ�

        player.SetUseGravity(gravity);

        // ���x���[���ɐݒ�
        player.SetVelocity(Vector2.zero);

        // �ǐG�ꎞ�̃p�[�e�B�N���G�t�F�N�g���Đ�
        playerParticleController.touchParticle.Play();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // �n�ʂɒ������ꍇ�A�A�C�h����Ԃɐ؂�ւ���
        if (player.IsGroundDetected())
            stateMachine.SwitchState(typeof(PlayerIdleState));

        // �ǂ��痣�ꂽ��A���͂����Ε����̏ꍇ�A������Ԃɐ؂�ւ���
        if (xInput == (player.facingDir * -1) || xInput == 0 || !player.IsWallDetected())
            stateMachine.SwitchState(typeof(PlayerFallState));

        // �W�����v���͂�����ꍇ�A�ǃW�����v��Ԃɐ؂�ւ���
        if (Jump && player.IsWallDetected())
        {
            player.Flip();
            stateMachine.SwitchState(typeof(PlayerWallJumpState));
        }

        // �o����͂�����ꍇ�A�o���Ԃɐ؂�ւ���
        if (Climb)
            stateMachine.SwitchState(typeof(PlayerClimbState));
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
