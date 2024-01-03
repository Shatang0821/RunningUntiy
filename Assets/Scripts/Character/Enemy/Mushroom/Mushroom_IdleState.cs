using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyState/Mushroom/Idle", fileName = "Mushroom_Idle")]
public class Mushroom_IdleState : EnemyState
{
    public override void Enter()
    {
        base.Enter();
        stateTimer = enemyBase.idleTime;

        Debug.Log("Idle");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (stateTimer < 0f)
            stateMachine.SwitchState(typeof(Mushroom_MoveState));
        
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
