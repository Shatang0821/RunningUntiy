using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/ClimbState", fileName = "PlayerState_Climb")]

public class PlayerClimbState : PlayerState
{
    [Header("Climb info")]
    [SerializeField] float gravity = 0;
    [SerializeField] float climbSpeed = 4;
    public override void Enter()
    {
        base.Enter();
        player.SetUseGravity(gravity);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(!Climb)
            stateMachine.SwitchState(typeof(PlayerIdleState));
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        player.SetVelocityY(yInput * climbSpeed);
    }
}
