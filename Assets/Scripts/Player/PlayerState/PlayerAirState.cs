using UnityEngine;

public class PlayerAirState : PlayerState
{
    [SerializeField] float moveSpeed = 2f;
    protected float currentFrame;
    public override void Enter()
    {
        base.Enter();
        currentFrame = 0;
        player.SetUseGravity(0);
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