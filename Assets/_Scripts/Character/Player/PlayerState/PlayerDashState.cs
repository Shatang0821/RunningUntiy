using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Dash", fileName = "PlayerState_Dash")]
public class PlayerDashState : PlayerState
{
    [Header("Dash Info")]
    [SerializeField] private float dashDuration;  // ダッシュの持続時間
    [SerializeField] private float dashSpeed;     // ダッシュ時のスピード
    [SerializeField] private Vector2 dashDir;     // ダッシュ方向
    private Vector2 facDashDir => new Vector2(player.facingDir, 0); //向く方向にダッシュ
    [Header("Dash Effect")]
    [SerializeField] private float dashEffectInterval = 0.02f; // ダッシュエフェクトの発生間隔
    private float releaseTimer; // 次のエフェクト生成までのタイマー
    public override void Enter()
    {
        base.Enter();
        AudioManager.Instance.PlaySFX(playerAudioController.dashSFX);
        releaseTimer = dashEffectInterval;

        dashTrigger = true;

        // ダッシュ時間を設定
        stateTimer = dashDuration;
        
        player.SetUseGravity(0);

        // カメラを揺らす
        CameraController.Instance.CameraShake(0.1f, 0.1f);

    }

    public override void Exit()
    {
        base.Exit();

        // プレイヤーの速度をリセット
        player.SetVelocity(Vector2.zero);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        // ダッシュ時間が終了した場合の処理
        if (stateTimer < 0)
        {
            // 地面にいない場合、落下状態に移行
            if (!player.IsGroundDetected())
                stateMachine.SwitchState(typeof(PlayerFallState));
            // 地面にいる場合
            if (player.IsGroundDetected())
            {
                //移動入力がなければアイドル状態に移行
                if (xInput == 0)
                    stateMachine.SwitchState(typeof(PlayerIdleState));
                else
                    //あれば移動状態
                    stateMachine.SwitchState(typeof(PlayerMoveState));
            }
        }

        // ダッシュエフェクトを生成するためのタイマー
        if (releaseTimer <= 0)
        {
            float alpha = 1 - (stateTimer % 6 / dashDuration);
            alpha = Mathf.Clamp(alpha, 0, 1); // alpha 値を 0 から 1 の間に保持
            PoolManager.Release(playerAction.dashGhost, player.transform.position,player.transform.rotation, playerAction.sprite, alpha);
            releaseTimer = dashEffectInterval; // タイマーをリセット
        }
        else
        {
            releaseTimer -= Time.deltaTime; // タイマーを更新
        }


            
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        // ダッシュ方向に速度を設定
        var dashDir = (dirInput == Vector2.zero) ? facDashDir : dirInput.normalized;
        player.SetVelocity(dashSpeed * dashDir);
    }

    /// <summary>
    /// 入力によってダッシュ方向を設定する
    /// コントローラのためである
    /// </summary>
    /// <returns>ダッシュ方向</returns>
    //Vector2 DirectionInput()
    //{
    //    // 入力がゼロかどうかをチェック
    //    if (input.Axis.x == 0 && input.Axis.y == 0)
    //    {
    //        return new Vector2(player.facingDir, 0);
    //    }

    //    // 入力から角度を計算
    //    float angleInRadians = Mathf.Atan2(input.Axis.y, input.Axis.x);
    //    float angleInDegrees = angleInRadians * Mathf.Rad2Deg;

    //    // 角度が負の場合、360度の範囲に変換
    //    if (angleInDegrees < 0)
    //    {
    //        angleInDegrees += 360;
    //    }

    //    // 角度で方向判断する
    //    if (angleInDegrees >= 337.5 || angleInDegrees < 22.5)
    //    {
    //        return new Vector2(1, 0); // 右
    //    }
    //    else if (angleInDegrees >= 22.5 && angleInDegrees < 67.5)
    //    {
    //        return new Vector2(1, 1); // 右上
    //    }
    //    else if (angleInDegrees >= 67.5 && angleInDegrees < 112.5)
    //    {
    //        return new Vector2(0, 1); // 上
    //    }
    //    else if (angleInDegrees >= 112.5 && angleInDegrees < 157.5)
    //    {
    //        return new Vector2(-1, 1); // 左上
    //    }
    //    else if (angleInDegrees >= 157.5 && angleInDegrees < 202.5)
    //    {
    //        return new Vector2(-1, 0); // 左
    //    }
    //    else if (angleInDegrees >= 202.5 && angleInDegrees < 247.5)
    //    {
    //        return new Vector2(-1, -1); // 左下
    //    }
    //    else if (angleInDegrees >= 247.5 && angleInDegrees < 292.5)
    //    {
    //        return new Vector2(0, -1); // 下
    //    }
    //    else if (angleInDegrees >= 292.5 && angleInDegrees < 337.5)
    //    {
    //        return new Vector2(1, -1); // 右下
    //    }
    //    else
    //        return Vector2.zero;
    //}
}
