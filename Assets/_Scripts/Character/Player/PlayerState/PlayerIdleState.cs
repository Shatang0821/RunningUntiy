using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Idle", fileName = "PlayerState_Idle")]
public class PlayerIdleState : PlayerGroundedState
{
    [Header("deceleration info")]
    [SerializeField] private int decelerationFrames = 3; // 減速に要するフレーム数
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
        if(stateMachine.CheckCurrentState(this)) return;
        
        // 入力があり、かつ地面にいる場合、移動状態に切り替える
        if (xInput != 0 && player.IsGroundDetected())
        {
            stateMachine.SwitchState(typeof(PlayerMoveState));
            return;
        }


        if (!player.IsGroundDetected())
        {
            stateMachine.SwitchState(typeof(PlayerFallState));
            return;
        }
            
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();

        // 減速処理。現在のフレームと減速に要するフレーム数を基に速度を調整
        ChangeVelocity(0, decelerationFrames);
    }
}
