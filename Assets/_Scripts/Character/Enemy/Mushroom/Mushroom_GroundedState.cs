public class Mushroom_GroundedState : EnemyState
{
    protected Enemy_Mushroom enemy;
    public Mushroom_GroundedState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName,Enemy_Mushroom _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

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
        if (enemy.deathTrigger == true)
            stateMachine.ChangeState(enemy.hitState);
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}