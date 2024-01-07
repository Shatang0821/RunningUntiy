using UnityEngine;

public class Mushroom_HitState : EnemyState
{
    protected Enemy_Mushroom enemy;
    public Mushroom_HitState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Mushroom _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        // カメラを揺らす
        CameraController.Instance.CameraShake(0.06f, 0.1f);

        enemy.LaunchAndDisable();
    }

    

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(triggerCalled == true)
            enemy.gameObject.SetActive(false);
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }


}
