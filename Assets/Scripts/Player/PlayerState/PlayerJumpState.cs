﻿using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Jump", fileName = "PlayerState_Jump")]
public class PlayerJumpState : PlayerAirState
{
    [SerializeField] private float JumpForce= 5;

    public override void Enter()
    {
        base.Enter();
        player.SetVelocityY(JumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(rb.velocity.y<=0)
            stateMachine.SwitchState(typeof(PlayerFallState));
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }

}
