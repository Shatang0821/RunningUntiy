using UnityEngine;

public class Mushroom_MoveState : Mushroom_GroundedState
{
    public Mushroom_MoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Mushroom _enemy) : base(_enemyBase, _stateMachine, _animBoolName, _enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //Debug.Log("Move");
    }

    public override void Exit()
    {
        base.Exit();
        enemyBase.SetVelocityX(0);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (enemyBase.IsWallDetected() || !enemyBase.IsGroundDetected())
        {
            enemyBase.Flip();
            stateMachine.ChangeState(enemy.idleState);
        }
            

    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        enemyBase.SetVelocityX(enemyBase.moveSpeed * enemyBase.facingDir);
    }
}
