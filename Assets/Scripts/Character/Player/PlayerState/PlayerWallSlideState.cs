using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/WallSlide", fileName = "PlayerState_WallSlide")]
public class PlayerWallSlideState : PlayerState
{
    [Header("WallSlide info")]
    [SerializeField] float gravity;
    public override void Enter()
    {
        
        base.Enter();
        //Debug.Log("Wall");
        player.SetUseGravity(gravity);
        player.SetVelocity(Vector2.zero);

        player.touchParticle.Play();
    }

    public override void Exit()
    {
        base.Exit();
        player.SetUseGravity(0.1f);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (player.IsGroundDetected())
            stateMachine.SwitchState(typeof(PlayerIdleState));
        if(xInput == (player.facingDir * -1) || xInput == 0 || !player.IsWallDetected())
            stateMachine.SwitchState(typeof(PlayerFallState));
        if (Jump && player.IsWallDetected())
        {
            player.Flip();
            stateMachine.SwitchState(typeof(PlayerWallJumpState));
        }
        if(Climb)
            stateMachine.SwitchState(typeof(PlayerClimbState));
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
