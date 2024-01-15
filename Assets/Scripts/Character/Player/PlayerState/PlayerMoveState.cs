using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Move", fileName = "PlayerState_Move")]
public class PlayerMoveState : PlayerGroundedState
{

    [Header("Move Info")]
    [SerializeField] private float moveSpeed;              // 移動速度
    [SerializeField] private int accelerationFrames = 6;   // 加速にかかるフレーム数
    float counter;                                         // 移動パーティクルエフェクトのタイマー
    float TargetVelocityX => xInput * moveSpeed;           // 目標のX軸上の速度


    public override void Enter()
    {
        base.Enter();
        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // 入力がなくなった場合、アイドル状態に切り替える
        if (xInput == 0)
            stateMachine.SwitchState(typeof(PlayerIdleState));

        // 地面を検出していない場合の処理
        if (!player.IsGroundDetected())
            stateMachine.SwitchState(typeof(PlayerCoyoteTimeState));
        // 移動パーティクルエフェクトのタイマーを更新
        counter += Time.deltaTime;
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();

        // 加速しながら目標速度に向けて速度を変更
        ChangeVelocity(TargetVelocityX, currentFrame, accelerationFrames);

        // 移動パーティクルエフェクトの再生間隔を制御
        if (counter > 0.1)
        {
            player.movementParticle.Play();
            counter = 0;
        }
        
    }

    
}
