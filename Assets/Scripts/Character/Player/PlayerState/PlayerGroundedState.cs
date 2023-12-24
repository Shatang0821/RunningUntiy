using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    private float coyoteTime;　  // コヨーテタイム（地面から離れた後にまだジャンプできる猶予時間）
    protected int currentFrame;　// 現在のフレーム数

    public override void Enter()
    {
        currentFrame = 0;

        coyoteTime = 0.08f; // コヨーテタイムを設定

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
        {
            // コヨーテタイム内であればジャンプ可能
            if (coyoteTime > 0)
            {
                coyoteTime -= Time.deltaTime;
                if (Jump)
                    stateMachine.SwitchState(typeof(PlayerJumpState));
                return;
            }
        }

        // 壁に接触していて、登る入力がある場合、登り状態に切り替える
        if (player.IsWallDetected() && Climb)
            stateMachine.SwitchState(typeof(PlayerClimbState));

        // 地面にいない場合、落下状態に切り替える
        if (!player.IsGroundDetected())
            stateMachine.SwitchState(typeof(PlayerFallState));

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