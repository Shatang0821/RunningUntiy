using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Dash", fileName = "PlayerState_Dash")]
public class PlayerDashState : PlayerState
{
    /*
     壁中ダッシュできない
     */
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashSpeed;
    [SerializeField] private Vector2 dashDir;
    private float releaseTimer = 0.02f;
    public override void Enter()
    {
        base.Enter();

        Debug.Log("dash");

        CheckDir();

        dashTrigger = true;

        stateTimer = dashDuration;

    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(new Vector2(0, 0), xInput);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (stateTimer < 0)
        {
            if(!player.IsGroundDetected())
                stateMachine.SwitchState(typeof(PlayerFallState));
            if (player.IsGroundDetected())
                stateMachine.SwitchState(typeof(PlayerIdleState));
        }

        // 添加一个计时器控制 PoolManager.Release 的调用频率
        if (releaseTimer <= 0)
        {
            float alpha = 1 - (stateTimer % 6 / dashDuration);
            alpha = Mathf.Clamp(alpha, 0, 1); // 确保 alpha 值在 0 到 1 之间
            PoolManager.Release(player.dashGhost, player.transform.position,player.transform.rotation, player.sprite, alpha);
            releaseTimer = 0.02f; // 重置计时器为 0.03 秒
        }
        else
        {
            releaseTimer -= Time.deltaTime; // 更新计时器
        }


            
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        player.SetVelocity(dashSpeed * dashDir,xInput);
    }

    void CheckDir()
    {

        dashDir = input.Axis;

        if (dashDir == Vector2.zero)
            dashDir = new Vector2(player.facingDir,0);
        //Debug.Log(dashDir);
    }
}
