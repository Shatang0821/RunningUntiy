using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/CustomJump", fileName = "PlayerState_CustomJump")]
public class PlayerCustomJumpState : PlayerAirState
{
    [Header("Jump info")]
    [SerializeField] private float jumpTime = 0.3f; //ジャンプ継続時間
    private float jumpForce;                        // ジャンプ力
    

    public override void Enter()
    {
        base.Enter();

        jumpForce = playerAction.customJumpForce;

        // プレイヤーにジャンプ力を適用
        player.SetVelocityY(jumpForce);

        // ジャンプ時のパーティクルエフェクトを再生
        playerParticleController.jumpParticle.Play();

        stateTimer = jumpTime;
    }


    public override void LogicUpdate()
    {
        // 入力に基づいてプレイヤーの向きを制御
        player.FlipController(xInput);

        base.LogicUpdate();
        if(stateMachine.CheckCurrentState(this)) return;
        // 上昇が終わり、下降を始めたら落下状態に切り替える
        if (rb.velocity.y <= 0)
        {
            stateMachine.SwitchState(typeof(PlayerFallState));
            return;
        }
            

        // 壁に接触していて、ジャンプ入力がある場合、壁ジャンプ状態に切り替える
        if ((playerAction.HasJumpInputBuffer || Jump) && player.IsWallDetected() && stateTimer < 0)
        {
            //向きを反転してから
            player.Flip();
            stateMachine.SwitchState(typeof(PlayerWallJumpState));
            return;
        }


    }

}
