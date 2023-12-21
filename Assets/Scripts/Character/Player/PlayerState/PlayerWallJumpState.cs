using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/WallJump", fileName = "PlayerState_WallJump")]
public class PlayerWallJumpState : PlayerAirState
{
    [SerializeField] private float jumpDuration;
    [SerializeField] private Vector2 jumpVelocity;
    public override void Enter()
    {
        //Debug.Log("PlayerWallJump");
        base.Enter();
        stateTimer = jumpDuration;
        //player.Flip();
        player.SetVelocity(jumpVelocity);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (stateTimer < 0)
            stateMachine.SwitchState(typeof(PlayerFallState));
    }

    public override void PhysicUpdate()
    {
        player.SetVelocityX(jumpVelocity.x * player.facingDir);
    }
}
