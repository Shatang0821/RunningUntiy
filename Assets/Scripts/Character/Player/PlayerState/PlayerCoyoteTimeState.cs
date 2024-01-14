using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/CoyoteTime", fileName = "PlayerState_Coyote")]
class PlayerCoyoteTimeState : PlayerState
{
    [SerializeField] float coyoteTime = 0.1f;
    [SerializeField] float moveSpeed;

    public override void Enter()
    {
        base.Enter();
        stateTimer = coyoteTime;
        player.SetUseGravity(0);
    }

    public override void Exit()
    {
        base.Exit();
        player.SetUseGravity(gravityBase);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (input.Jump)
            stateMachine.SwitchState(typeof(PlayerJumpState));

        if (stateTimer < 0)
            stateMachine.SwitchState(typeof(PlayerFallState));
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        player.SetVelocityX(xInput * moveSpeed);
    }
}