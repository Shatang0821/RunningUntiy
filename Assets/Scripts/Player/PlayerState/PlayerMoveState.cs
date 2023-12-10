using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Move", fileName = "PlayerState_Move")]
public class PlayerMoveState : PlayerGroundedState
{

    [Header("Move info")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private int accelerationFrames = 6;//加速フレーム

    float targetVelocityX;

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Move");
        targetVelocityX = xInput * moveSpeed;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (xInput == 0)
            stateMachine.SwitchState(typeof(PlayerIdleState));
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        ChangeVelocity(targetVelocityX, currentFrame, accelerationFrames);
    }

    
}
