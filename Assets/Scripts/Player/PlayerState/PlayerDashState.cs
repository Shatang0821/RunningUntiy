using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Dash", fileName = "PlayerState_Dash")]
public class PlayerDashState : PlayerState
{
    /*
     •Ç’†ƒ_ƒbƒVƒ…‚Å‚«‚È‚¢
     */
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashSpeed;
    [SerializeField] private Vector2 dashDir;
    public override void Enter()
    {
        base.Enter();

        Debug.Log("dash");

        CheckDir();

        player.dashTrigger = true;

        stateTimer = dashDuration;
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(new Vector2(0, 0));
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (stateTimer < 0)
        {
            if(!player.IsGroundDetected())
                stateMachine.SwitchState(typeof(PlayerFallState));
            if (player.IsGroundDetected())
                stateMachine.SwitchState(typeof(PlayerIdleState));
        }
        if (player.IsWallDetected())
            stateMachine.SwitchState(typeof(PlayerWallSlideState));
            
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        player.SetVelocity(dashSpeed * dashDir);
    }

    void CheckDir()
    {
        dashDir = input.Axis.normalized;

        if (dashDir == Vector2.zero)
            dashDir = new Vector2(player.facingDir,0);
        //Debug.Log(dashDir);
    }
}
