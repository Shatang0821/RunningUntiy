using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/ClimbState", fileName = "PlayerState_Climb")]

public class PlayerClimbState : PlayerState
{
    // ジャンプしたい方向を示す
    private enum JumpDirection
    {
        /// <summary>
        /// 壁側
        /// </summary>
        TowardsWall = 1,    
        /// <summary>
        /// 壁との反対側
        /// </summary>
        AwayFromWall = -1
    }
    [Header("Climb info")]
    [SerializeField] float gravity = 0;         //重力
    [SerializeField] float climbSpeed = 4;      //登るスピード

    JumpDirection jumpDir;                             //ジャンプ方向
    public override void Enter()
    {
        base.Enter();
        //壁に登るときの重力を設定する
        player.SetUseGravity(gravity);

        //パーティクルをプレイ
        playerParticleController.touchParticle.Play();
    }

    public override void Exit()
    {
        base.Exit();
        //速度をリセット
        player.SetVelocityY(0);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //Debug.Log(xInput);

        SetFacingDir();
        // プレイヤーが登っていない場合、PlayerIdleState状態に切り替える。
        if (!Climb)
            stateMachine.SwitchState(typeof(PlayerIdleState));

        // プレイヤーが壁を検出し、入力方向がプレイヤーの向いている方向と一致し、かつ登っていない場合、PlayerWallSlideState状態に切り替える。
        if (player.IsWallDetected() && xInput == player.facingDir && !Climb)
            stateMachine.SwitchState(typeof(PlayerWallSlideState));

        // プレイヤーがジャンプ中に壁を検出した場合。
        if (Jump && player.IsWallDetected())
        {
            // ジャンプ方向がプレイヤーの向いている方向と逆の場合
            if (jumpDir == JumpDirection.AwayFromWall)
            {
                //プレイヤーを反転させて
                player.Flip();
                //PlayerWallJumpState状態に切り替える。
                stateMachine.SwitchState(typeof(PlayerWallJumpState));
            }
            //そうでなければプレイヤーのを反転せず
            else
                stateMachine.SwitchState(typeof(PlayerWallJumpState));
        }

        // プレイヤーが壁を検出していない場合、PlayerFallState状態に切り替える。
        //要修正
        if (!player.IsWallDetected())
            stateMachine.SwitchState(typeof(PlayerFallState));

        //登る途中で壁を検出していない場合、PlayerClimbLeapState状態に切り替える
        if (!player.IsWallDetected() && yInput > 0 && !Jump)
            stateMachine.SwitchState(typeof(PlayerClimbLeapState));
            
    }


    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        //壁を検出した場合登れるようにする
        if(player.IsWallDetected() && jumpDir == JumpDirection.TowardsWall)
            player.SetVelocityY(yInput * climbSpeed);
        else
            player.SetVelocityY(0);
    }

    void SetFacingDir()
    {
        // ジャンプ方向の設定...
        if (player.facingDir == xInput || xInput == 0)
        {
            jumpDir = JumpDirection.TowardsWall;
        }
        else
        {
            jumpDir = JumpDirection.AwayFromWall;
        }
        // アニメーションパラメータを設定して、ジャンプ時のプレイヤーの向きを決定する。
        player.anim.SetFloat("playerFacing", (float)jumpDir);
    }
}
