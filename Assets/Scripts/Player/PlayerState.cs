using UnityEngine;

public class PlayerState : ScriptableObject, IState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;

    [SerializeField] private string animBoolName;
    protected int stateBoolHash;

    protected float stateTimer;

    protected bool triggerCalled;

    private void OnEnable()
    {
        stateBoolHash = Animator.StringToHash(animBoolName);
    }

    public void Initialize(Player _player,PlayerStateMachine _stateMachine)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
    }

    public virtual void Enter()
    {
        player.anim.SetBool(stateBoolHash, true);
    }

    public virtual void Exit()
    {
        player.anim.SetBool(stateBoolHash, false);
    }

    public virtual void LogicUpdate()
    {
        
    }

    public virtual void PhysicUpdate()
    {
        
    }
}
