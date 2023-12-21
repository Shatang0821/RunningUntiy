using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/ClimbState", fileName = "PlayerState_Climb")]

public class PlayerClimbState : PlayerState
{
    [Header("Climb info")]
    [SerializeField] float gravity = 0;
    [SerializeField] float climbSpeed = 4;
    int jumpDir;
    public override void Enter()
    {
        base.Enter();
        player.SetUseGravity(gravity);
    }

    public override void Exit()
    {
        base.Exit();
        player.SetUseGravity(0.1f);
        player.SetVelocityY(0);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        jumpDir = (player.facingDir == xInput || xInput == 0) ? 1 : -1;

        player.anim.SetFloat("playerFacing", jumpDir);

        if(!Climb)
            stateMachine.SwitchState(typeof(PlayerIdleState));
        if (player.IsWallDetected() && xInput == player.facingDir && !Climb)
            stateMachine.SwitchState(typeof(PlayerWallSlideState));
        if (Jump && player.IsWallDetected())
        {
            if(jumpDir == -1)
            {
                player.Flip();
                stateMachine.SwitchState(typeof(PlayerWallJumpState));
            }
            else
                stateMachine.SwitchState(typeof(PlayerWallJumpState));
        }
        if (!player.IsWallDetected())
            stateMachine.SwitchState(typeof(PlayerFallState));
            

    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        player.SetVelocityY(yInput * climbSpeed);
    }
}
