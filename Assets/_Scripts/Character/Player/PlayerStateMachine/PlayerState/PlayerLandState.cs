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
        // ジャンプ入力バッファがある場合、ジャンプ状態に切り替える
        if (playerAction.HasJumpInputBuffer)
        {
            stateMachine.SwitchState(typeof(PlayerJumpState));
            return;
        }

        // 地面にいない場合、落下状態に切り替える
        if (!player.IsGroundDetected())
        {
            stateMachine.SwitchState(typeof(PlayerFallState));
            return;
        }
            

        // 入力がない場合、アイドル状態に切り替える
        if (xInput == 0)
        {
            stateMachine.SwitchState(typeof(PlayerIdleState));
            return;
        }
            

        // 入力があり、かつ地面にいる場合、移動状態に切り替える
        if (xInput != 0 && player.IsGroundDetected())
        {
            stateMachine.SwitchState(typeof(PlayerMoveState));
            return;
        }
            
    }

}
