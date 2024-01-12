using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/ClimbLeap", fileName = "PlayerState_ClimbLeap")]
public class PlayerClimbLeapState : PlayerAirState
{
    [Header("Jump info")]
    [SerializeField] private float JumpForce = 2;    // ジャンプ力
    [SerializeField] private float jumpTime = 0.3f;

    public override void Enter()
    {
        base.Enter();

        // プレイヤーにジャンプ力を適用
        player.SetVelocityY(JumpForce);

        // ジャンプ時のパーティクルエフェクトを再生
        player.jumpParticle.Play();

        stateTimer = jumpTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        // 入力に基づいてプレイヤーの向きを制御
        player.FlipController(xInput);

        base.LogicUpdate();

        // 上昇が終わり、下降を始めたら落下状態に切り替える
        if (rb.velocity.y <= 0)
            stateMachine.SwitchState(typeof(PlayerFallState));

        // 壁に接触していて、ジャンプ入力がある場合、壁ジャンプ状態に切り替える
        if (Jump && player.IsWallDetected() && stateTimer < 0)
        {
            //向きを反転してから
            player.Flip();
            stateMachine.SwitchState(typeof(PlayerWallJumpState));
        }


    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }

}
