using UnityEngine;

public class PlayerAirState : PlayerState
{
    [Header("AirMove info")]
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float gravity;

    public override void Enter()
    {
        base.Enter();
        //重力の設定
        player.SetUseGravity(gravity);
    }
    
    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        //空中でも移動できるようにする
        player.SetVelocityX(xInput * moveSpeed);
    }
}