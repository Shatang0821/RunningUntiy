using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/WallSlide", fileName = "PlayerState_WallSlide")]
public class PlayerWallSlideState : PlayerState
{
    [Header("WallSlide info")]
    [SerializeField] float gravity; // 壁滑り時の重力
    public override void Enter()
    {
        
        base.Enter();

        // 壁に触れた時の重力を設定

        player.SetUseGravity(gravity);

        // 速度をゼロに設定
        player.SetVelocity(Vector2.zero);

        // 壁触れ時のパーティクルエフェクトを再生
        playerParticleController.touchParticle.Play();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // 地面に着いた場合、アイドル状態に切り替える
        if (player.IsGroundDetected())
            stateMachine.SwitchState(typeof(PlayerIdleState));

        // 壁から離れたり、入力が反対方向の場合、落下状態に切り替える
        if (xInput == (player.facingDir * -1) || xInput == 0 || !player.IsWallDetected())
            stateMachine.SwitchState(typeof(PlayerFallState));

        // ジャンプ入力がある場合、壁ジャンプ状態に切り替える
        if (Jump && player.IsWallDetected())
        {
            player.Flip();
            stateMachine.SwitchState(typeof(PlayerWallJumpState));
        }

        // 登る入力がある場合、登り状態に切り替える
        if (Climb)
            stateMachine.SwitchState(typeof(PlayerClimbState));
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
