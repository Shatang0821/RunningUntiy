using UnityEngine;

public class PlayerAirState : PlayerState
{
    [Header("AirMove info")]
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float gravity;

    public override void Enter()
    {
        base.Enter();
        //�d�͂̐ݒ�
        player.SetUseGravity(gravity);
    }

    //�d�͂���ɖ߂�
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
        //�󒆂ł��ړ��ł���悤�ɂ���
        player.SetVelocityX(xInput * moveSpeed);
    }
}