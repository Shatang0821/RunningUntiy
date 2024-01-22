using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/WallJump", fileName = "PlayerState_WallJump")]
public class PlayerWallJumpState : PlayerAirState
{
    [SerializeField] private float jumpDuration;  // 壁ジャンプの持続時間
    [SerializeField] private Vector2 jumpVelocity; // 壁ジャンプ時の速度

    public override void Enter()
    {
        //Debug.Log("PlayerWallJump");
        base.Enter();
        AudioManager.Instance.PlaySFX(playerAudioController.jumpSFX);
        // ジャンプの持続時間を設定
        stateTimer = jumpDuration;
        // 壁から離れる方向に速度を設定
        player.SetVelocity(jumpVelocity);

        // ジャンプ時のパーティクルエフェクトを再生
        playerParticleController.jumpParticle.Play();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // ジャンプの持続時間が終了したら、落下状態に切り替える
        if (stateTimer < 0)
            stateMachine.SwitchState(typeof(PlayerFallState));
    }

    public override void PhysicUpdate()
    {
        // 壁ジャンプ時のX軸方向の速度を設定（プレイヤーの向きに依存）
        player.SetVelocityX(jumpVelocity.x * player.facingDir);
    }
}
