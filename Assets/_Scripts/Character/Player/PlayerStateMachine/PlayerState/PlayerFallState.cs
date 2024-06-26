using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Fall", fileName = "PlayerState_Fall")]
public class PlayerFallState : PlayerAirState
{
    [SerializeField] float maxFallSpeed;        //落下の最大速度を制限する

    public override void LogicUpdate()
    {
        // 入力に基づいてプレイヤーの向きを制御
        player.FlipController(xInput);

        base.LogicUpdate();
        if(stateMachine.CheckCurrentState(this)) return;
        
        // 地面に着地したかチェックし、着地状態に切り替える
        if (player.IsGroundDetected())
        {
            stateMachine.SwitchState(typeof(PlayerLandState));
            return;
        }
           
        // ジャンプ入力がある場合、ジャンプバッファタイマーを設定
        if (Jump && !player.IsWallDetected())
        {
            playerAction.SetJumpInputBufferTimer();
            return;
        }
            

        // 壁に接触していて、入力方向がプレイヤーの向いている方向と一致する場合、壁滑り状態に切り替える
        if (player.IsWallDetected() && xInput == player.facingDir)
        {
            stateMachine.SwitchState(typeof(PlayerWallSlideState));
            return;
        }
            

        // 壁に接触していて、ジャンプ入力がある場合、壁ジャンプ状態に切り替える
        if (Jump && player.IsWallDetected())
        {
            //反対側にジャンプするため向きを変更
            player.Flip();
            stateMachine.SwitchState(typeof(PlayerWallJumpState));
            return;
        }

        // 壁に接触していて、登る入力がある場合、登り状態に切り替える
        if (player.IsWallDetected() && Climb)
        {
            stateMachine.SwitchState(typeof (PlayerClimbState));  
            return;
        }
              
            
            
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();

        // 落下速度を制限する処理。速度が最大落下速度を超えないように調整
        float newFallSpeed = Mathf.Clamp(rb.velocity.y, maxFallSpeed, float.MaxValue);
        rb.velocity = new Vector2(rb.velocity.x, newFallSpeed);
    }

}