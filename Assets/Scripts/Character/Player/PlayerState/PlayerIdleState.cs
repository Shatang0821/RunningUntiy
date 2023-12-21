using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Idle", fileName = "PlayerState_Idle")]
public class PlayerIdleState : PlayerGroundedState
{
    [Header("deceleration info")]
    [SerializeField] private int decelerationFrames = 3;
    public override void Enter()
    {
        base.Enter();
        //Debug.Log("Idle");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (xInput != 0 && player.IsGroundDetected())
            stateMachine.SwitchState(typeof(PlayerMoveState));
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        ChangeVelocity(0, currentFrame, decelerationFrames);
    }
}
