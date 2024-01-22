using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/WallJump", fileName = "PlayerState_WallJump")]
public class PlayerWallJumpState : PlayerAirState
{
    [SerializeField] private float jumpDuration;  // �ǃW�����v�̎�������
    [SerializeField] private Vector2 jumpVelocity; // �ǃW�����v���̑��x

    public override void Enter()
    {
        //Debug.Log("PlayerWallJump");
        base.Enter();
        AudioManager.Instance.PlaySFX(playerAudioController.jumpSFX);
        // �W�����v�̎������Ԃ�ݒ�
        stateTimer = jumpDuration;
        // �ǂ��痣�������ɑ��x��ݒ�
        player.SetVelocity(jumpVelocity);

        // �W�����v���̃p�[�e�B�N���G�t�F�N�g���Đ�
        playerParticleController.jumpParticle.Play();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // �W�����v�̎������Ԃ��I��������A������Ԃɐ؂�ւ���
        if (stateTimer < 0)
            stateMachine.SwitchState(typeof(PlayerFallState));
    }

    public override void PhysicUpdate()
    {
        // �ǃW�����v����X�������̑��x��ݒ�i�v���C���[�̌����Ɉˑ��j
        player.SetVelocityX(jumpVelocity.x * player.facingDir);
    }
}
