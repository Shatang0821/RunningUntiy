using UnityEngine;

public class PlayerAirState : PlayerState
{
    [Header("AirMove info")]
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float gravity;

    public override void Enter()
    {
        base.Enter();
        //d—Í‚Ìİ’è
        player.SetUseGravity(gravity);
    }

    //d—Í‚ğŠî‚É–ß‚·
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
        //‹ó’†‚Å‚àˆÚ“®‚Å‚«‚é‚æ‚¤‚É‚·‚é
        player.SetVelocityX(xInput * moveSpeed);
    }
}