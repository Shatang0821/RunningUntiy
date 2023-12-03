using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Fall", fileName = "PlayerState_Fall")]
public class PlayerFallState : PlayerAirState
{
    [SerializeField] private float JumpForce = -5;
    public override void Enter()
    {
        base.Enter();
        player.SetVelocityY(JumpForce);
    }

    public override void Exit()
    {
        base.Exit();
        player.SetUseGravity(1);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (player.IsGroundDetected())
            stateMachine.SwitchState(typeof(PlayerIdleState));
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}