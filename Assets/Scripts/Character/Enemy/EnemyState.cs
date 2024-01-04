using UnityEngine;

public class EnemyState : IState
{
    protected EnemyStateMachine stateMachine;// 状態マシン
    protected Enemy enemyBase;
    protected Rigidbody2D rb;


    #region Animator
    [SerializeField] private string animBoolName;
    #endregion

    #region State
    protected float stateTimer;
    protected bool triggerCalled;

    #endregion


    public EnemyState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName)
    {
        this.enemyBase = _enemyBase;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        enemyBase.anim.SetBool(animBoolName, true); // アニメーターの状態を更新
        rb = enemyBase.rb;
    }

    public virtual void Exit()
    {
        enemyBase.anim.SetBool(animBoolName, false); // アニメーターの状態を更新
    }

    public virtual void LogicUpdate()
    {
        stateTimer -= Time.deltaTime;             // 状態タイマーの更新
    }

    public virtual void PhysicUpdate()
    {
        //NONE
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
