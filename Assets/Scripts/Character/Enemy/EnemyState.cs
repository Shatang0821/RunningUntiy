using UnityEngine;

public class EnemyState : ScriptableObject,IState
{
    protected EnemyStateMachine stateMachine;// 状態マシン
    protected Enemy enemyBase;

    protected Rigidbody2D rb;

    #region Animator
    [SerializeField] private string animBoolName;
    protected int stateBoolHash;
    #endregion

    #region State
    protected float stateTimer;

    //protected bool triggerCaleed;
    #endregion

    private void OnEnable()
    {
        stateBoolHash = Animator.StringToHash(animBoolName);
    }

    public void Initialize(Enemy _enemy,EnemyStateMachine _stateMachine)
    {
        this.enemyBase = _enemy;
        this.stateMachine = _stateMachine;
    }

    public virtual void Enter()
    {
        enemyBase.anim.SetBool(stateBoolHash, true); // アニメーターの状態を更新
        rb = enemyBase.rb;
    }

    public virtual void Exit()
    {
        enemyBase.anim.SetBool(stateBoolHash, false); // アニメーターの状態を更新
    }

    public virtual void LogicUpdate()
    {
        stateTimer -= Time.deltaTime;             // 状態タイマーの更新
    }

    public virtual void PhysicUpdate()
    {
        //NONE
    }
}
