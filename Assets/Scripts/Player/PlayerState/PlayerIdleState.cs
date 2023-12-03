using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Idle", fileName = "PlayerState_Idle")]
public class PlayerIdleState : PlayerGroundedState
{
    [SerializeField] private int decelerationFrames = 3;
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
        if (xInput != 0)
            stateMachine.SwitchState(typeof(PlayerMoveState));
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        ChangeVelocity(0, currentFrame, decelerationFrames);
    }
}
