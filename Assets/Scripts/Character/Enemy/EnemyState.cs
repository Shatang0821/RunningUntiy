using UnityEngine;

public class EnemyState : ScriptableObject,IState
{
    protected EnemyStateMachine stateMachine;// ��ԃ}�V��
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
        enemyBase.anim.SetBool(stateBoolHash, true); // �A�j���[�^�[�̏�Ԃ��X�V
        rb = enemyBase.rb;
    }

    public virtual void Exit()
    {
        enemyBase.anim.SetBool(stateBoolHash, false); // �A�j���[�^�[�̏�Ԃ��X�V
    }

    public virtual void LogicUpdate()
    {
        stateTimer -= Time.deltaTime;             // ��ԃ^�C�}�[�̍X�V
    }

    public virtual void PhysicUpdate()
    {
        //NONE
    }
}
