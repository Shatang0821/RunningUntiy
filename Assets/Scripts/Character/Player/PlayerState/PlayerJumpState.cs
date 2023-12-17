using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Jump", fileName = "PlayerState_Jump")]
public class PlayerJumpState : PlayerAirState
{
    [Header("Jump info")]
    [SerializeField] private float JumpForce= 5;

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Jump");
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
        if (Jump && player.IsWallDetected())
            stateMachine.SwitchState(typeof(PlayerWallJumpState));

    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }

}
