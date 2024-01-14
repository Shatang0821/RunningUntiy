using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int currentFrame;　// 現在のフレーム数

    public override void Enter()
    {
        currentFrame = 0;

        // ダッシュトリガーをリセット
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

        // 入力に基づいてプレイヤーの向きを制御
        player.FlipController(xInput);

        // ジャンプ入力がある場合、ジャンプ状態に切り替える
        if (Jump)
            stateMachine.SwitchState(typeof(PlayerJumpState));

        // 地面を検出していない場合の処理
        if (!player.IsGroundDetected())
            stateMachine.SwitchState(typeof(PlayerCoyoteTimeState));

        // 壁に接触していて、登る入力がある場合、登り状態に切り替える
        if (player.IsWallDetected() && Climb)
            stateMachine.SwitchState(typeof(PlayerClimbState));
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        currentFrame++; // 現在のフレーム数を更新
    }

    /// <summary>
    /// フレーム単位で速度を制御するメソッド
    /// </summary>
    /// <param name="targetSpeed">目標速度</param>
    /// <param name="currentFrame">現在のフレーム数</param>
    /// <param name="totalFrames">最高速度に達するまでのフレーム数</param>
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