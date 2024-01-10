using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/ClimbState", fileName = "PlayerState_Climb")]

public class PlayerClimbState : PlayerState
{
    [Header("Climb info")]
    [SerializeField] float gravity = 0;         //重力
    [SerializeField] float climbSpeed = 4;      //登るスピード

    int jumpDir;                                //ジャンプ方向
    public override void Enter()
    {
        base.Enter();
        //壁に登るときの重力を設定する
        player.SetUseGravity(gravity);

        //パーティクルをプレイ
        player.touchParticle.Play();
    }

    public override void Exit()
    {
        base.Exit();
        player.SetUseGravity(gravityBase);
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
            if (jumpDir == -1)
            {
                //プレイヤーを反転させ
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

        if(!player.IsWallDetected()&& yInput > 0)
            stateMachine.SwitchState(typeof(PlayerJumpState));
    }


    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        //壁を検出した場合登れるようにする
        if(player.IsWallDetected() && jumpDir == 1)
            player.SetVelocityY(yInput * climbSpeed);
        else
            player.SetVelocityY(0);
    }

    void SetFacingDir()
    {
        
        //Debug.Log(Dir());
        // ジャンプの方向を判断する。プレイヤーが向いている方向と入力方向が同じ、または入力がない場合、ジャンプ方向は1、そうでない場合は-1とする。
        jumpDir = (player.facingDir == Dir() || Dir() == 0) ? 1 : -1;
        // アニメーションパラメータを設定して、ジャンプ時のプレイヤーの向きを決定する。
        player.anim.SetFloat("playerFacing", jumpDir);
    }

    int Dir()
    {
        if(input.Axis.x == 0)
            return 0;

        // 入力方向の角度を取得
        float angleInDegrees = Mathf.Atan2(input.Axis.y, input.Axis.x) * Mathf.Rad2Deg;
        if (angleInDegrees < 0) angleInDegrees += 360;

        if (angleInDegrees >= 135 && angleInDegrees < 225)
        {
            return -1;
        }
        else if (angleInDegrees >= 315 || angleInDegrees < 45)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
