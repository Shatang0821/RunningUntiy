using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyState/Mushroom/Move", fileName = "Mushroom_Move")]
public class Mushroom_MoveState : EnemyState
{
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Move");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (enemyBase.IsWallDetected() || !enemyBase.IsGroundDetected())
        {
            enemyBase.Flip();
            stateMachine.SwitchState(typeof(Mushroom_IdleState));
        }
            

    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        enemyBase.SetVelocityX(enemyBase.moveSpeed * enemyBase.facingDir);
    }
}
