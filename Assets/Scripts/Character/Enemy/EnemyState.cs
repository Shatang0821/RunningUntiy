using UnityEngine;

public class EnemyState : IState
{
    protected EnemyStateMachine stateMachine;// ��ԃ}�V��
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
        enemyBase.anim.SetBool(animBoolName, true); // �A�j���[�^�[�̏�Ԃ��X�V
        rb = enemyBase.rb;
    }

    public virtual void Exit()
    {
        enemyBase.anim.SetBool(animBoolName, false); // �A�j���[�^�[�̏�Ԃ��X�V
    }

    public virtual void LogicUpdate()
    {
        stateTimer -= Time.deltaTime;             // ��ԃ^�C�}�[�̍X�V
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
