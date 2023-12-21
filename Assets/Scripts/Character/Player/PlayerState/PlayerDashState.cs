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

        //Debug.Log("dash");

        CheckDir();

        dashTrigger = true;

        stateTimer = dashDuration;

        CameraController.Instance.CameraShake(0.1f, 0.1f);

    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(new Vector2(0, 0));
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
        player.SetVelocity(dashSpeed * dashDir);
    }

    void CheckDir()
    {
        dashDir = DirectionInput().normalized;
        Debug.Log(dashDir);
    }

    Vector2 DirectionInput()
    {
        // 先检查输入是否为零
        if (input.Axis.x == 0 && input.Axis.y == 0)
        {
            return new Vector2(player.facingDir, 0);
        }

        float angleInRadians = Mathf.Atan2(input.Axis.y, input.Axis.x);
        float angleInDegrees = angleInRadians * Mathf.Rad2Deg;

        // 如果角度为负数，将其转换为 0 到 360 度的范围
        if (angleInDegrees < 0)
        {
            angleInDegrees += 360;
        }

        Debug.Log(angleInDegrees);
        // 角度で方向判断する
        if (angleInDegrees >= 337.5 || angleInDegrees < 22.5)
        {
            return new Vector2(1, 0); // 右
        }
        else if (angleInDegrees >= 22.5 && angleInDegrees < 67.5)
        {
            return new Vector2(1, 1); // 右上
        }
        else if (angleInDegrees >= 67.5 && angleInDegrees < 112.5)
        {
            return new Vector2(0, 1); // 上
        }
        else if (angleInDegrees >= 112.5 && angleInDegrees < 157.5)
        {
            return new Vector2(-1, 1); // 左上
        }
        else if (angleInDegrees >= 157.5 && angleInDegrees < 202.5)
        {
            return new Vector2(-1, 0); // 左
        }
        else if (angleInDegrees >= 202.5 && angleInDegrees < 247.5)
        {
            return new Vector2(-1, -1); // 左下
        }
        else if (angleInDegrees >= 247.5 && angleInDegrees < 292.5)
        {
            return new Vector2(0, -1); // 下
        }
        else if (angleInDegrees >= 292.5 && angleInDegrees < 337.5)
        {
            return new Vector2(1, -1); // 右下
        }
        else
            return Vector2.zero;
    }
}
