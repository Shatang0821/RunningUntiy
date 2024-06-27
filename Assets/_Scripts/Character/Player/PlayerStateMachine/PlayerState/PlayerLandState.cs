using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Land", fileName = "PlayerState_Land")]
public class PlayerLandState : PlayerState
{
    public override void Enter()
    {
        base.Enter();

        playerParticleController.fallParticle.Play();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(stateMachine.CheckCurrentState(this)) return;
        // �W�����v���̓o�b�t�@������ꍇ�A�W�����v��Ԃɐ؂�ւ���
        if (playerAction.HasJumpInputBuffer)
        {
            stateMachine.SwitchState(typeof(PlayerJumpState));
            return;
        }

        // �n�ʂɂ��Ȃ��ꍇ�A������Ԃɐ؂�ւ���
        if (!player.IsGroundDetected())
        {
            stateMachine.SwitchState(typeof(PlayerFallState));
            return;
        }
            

        // ���͂��Ȃ��ꍇ�A�A�C�h����Ԃɐ؂�ւ���
        if (xInput == 0)
        {
            stateMachine.SwitchState(typeof(PlayerIdleState));
            return;
        }
            

        // ���͂�����A���n�ʂɂ���ꍇ�A�ړ���Ԃɐ؂�ւ���
        if (xInput != 0 && player.IsGroundDetected())
        {
            stateMachine.SwitchState(typeof(PlayerMoveState));
            return;
        }
            
    }

}