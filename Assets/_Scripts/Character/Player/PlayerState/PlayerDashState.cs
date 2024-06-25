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
    [SerializeField] private float dashEffectInterval = 0.02f;  // ダッシュエフェクトの発生間隔
    private float releaseTimer;                                 // 次のエフェクト生成までのタイマー
    public override void Enter()
    {
        base.Enter();
        AudioManager.Instance.PlaySFX(playerAudioController.dashSFX);
        releaseTimer = dashEffectInterval;

        EventCenter.TriggerEvent(StateEvents.SetDashTrigger, false);

        // ダッシュ時間を設定
        stateTimer = dashDuration;
        
        player.SetUseGravity(0);

        // ダッシュ方向に速度を設定
        dashDir = (dirInput == Vector2.zero) ? facDashDir : dirInput.normalized;

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
        if(stateMachine.CheckCurrentState(this)) return;
        
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
        
        // ダッシュ時間が終了した場合の処理
        if (stateTimer < 0)
        {
            // 地面にいない場合、落下状態に移行
            if (!player.IsGroundDetected())
            {
                stateMachine.SwitchState(typeof(PlayerFallState));
                return;
            }
                
            // 地面にいる場合
            if (player.IsGroundDetected())
            {
                //移動入力がなければアイドル状態に移行
                if (xInput == 0)
                {
                    stateMachine.SwitchState(typeof(PlayerIdleState));
                    return;
                }

                else
                {
                    //あれば移動状態
                    stateMachine.SwitchState(typeof(PlayerMoveState));
                    return;
                }
                    
            }
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        // ダッシュ方向に速度を設定
        player.SetVelocity(dashSpeed * dashDir);
    }
}
