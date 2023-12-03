using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Fall", fileName = "PlayerState_Fall")]
public class PlayerFallState : PlayerAirState
{
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
        if (player.IsGroundDetected())
            stateMachine.SwitchState(typeof(PlayerIdleState));
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}