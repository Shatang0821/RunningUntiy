using UnityEngine;

public class PlayerState : ScriptableObject, IState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;

    protected Rigidbody2D rb;

    #region Animator
    [SerializeField] private string animBoolName;
    protected int stateBoolHash;
    #endregion

    #region State
    protected float stateTimer;

    protected bool triggerCalled;
    #endregion

    #region Input
    protected PlayerInput input;
    protected float xInput => input.Axis.x;
    protected float yInput => input.Axis.y;
    #endregion

    private void OnEnable()
    {
        stateBoolHash = Animator.StringToHash(animBoolName);
    }

    public void Initialize(Player _player,PlayerStateMachine _stateMachine,PlayerInput _input)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.input = _input;
    }

    public virtual void Enter()
    {
        player.anim.SetBool(stateBoolHash, true);
        rb = player.rb;
    }

    public virtual void Exit()
    {
        player.anim.SetBool(stateBoolHash, false);
    }

    public virtual void LogicUpdate()
    {
        stateTimer -= Time.deltaTime;
    }

    public virtual void PhysicUpdate()
    {
        
    }
}
