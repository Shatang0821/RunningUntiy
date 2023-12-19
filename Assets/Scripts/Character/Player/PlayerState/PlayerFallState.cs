using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Fall", fileName = "PlayerState_Fall")]
public class PlayerFallState : PlayerAirState
{
    public override void Enter()
    {
        base.Enter();
        //Debug.Log("Fall");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (player.IsGroundDetected())
            stateMachine.SwitchState(typeof(PlayerLandState));
        if (Jump && !player.IsWallDetected())
            player.SetJumpInputBufferTimer();
        if (player.IsWallDetected()&&xInput ==player.facingDir)
            stateMachine.SwitchState(typeof(PlayerWallSlideState));
        if(Jump && player.IsWallDetected())
            stateMachine.SwitchState(typeof(PlayerWallJumpState));
            
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}