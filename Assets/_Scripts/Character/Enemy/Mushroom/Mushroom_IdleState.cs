using UnityEngine;

public class Mushroom_IdleState : Mushroom_GroundedState
{
    public Mushroom_IdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Mushroom _enemy) : base(_enemyBase, _stateMachine, _animBoolName,_enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = enemyBase.idleTime;

        //Debug.Log("Idle");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (stateTimer < 0f)
            stateMachine.ChangeState(enemy.moveState);
        
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
