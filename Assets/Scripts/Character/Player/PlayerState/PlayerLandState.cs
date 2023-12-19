using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Land", fileName = "PlayerState_Land")]
public class PlayerLandState : PlayerState
{
    public override void Enter()
    {
        base.Enter();
        //Debug.Log("Land");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (player.HasJumpInputBuffer)
        {
            stateMachine.SwitchState(typeof(PlayerJumpState));
            return;
        }
        if(xInput ==0 )
            stateMachine.SwitchState(typeof(PlayerIdleState));
        if (xInput != 0 && player.IsGroundDetected())
            stateMachine.SwitchState(typeof(PlayerMoveState));
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
