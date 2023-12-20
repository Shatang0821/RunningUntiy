using UnityEngine;

public class PlayerAirState : PlayerState
{
    [Header("AirMove info")]
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float gravity;
    public override void Enter()
    {
        base.Enter();
        player.SetUseGravity(gravity);
    }

    public override void Exit()
    {
        base.Exit();
        player.SetUseGravity(0.1f);
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