using UnityEngine;

public class PlayerAirState : PlayerState
{
    [SerializeField] float moveSpeed = 2f;
    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        player.SetVelocityX(xInput * moveSpeed);
    }
}