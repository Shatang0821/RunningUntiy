using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/PlayerState", fileName = "PlayerState")]
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

    protected bool Jump => input.Jump;
    protected bool Dash => input.Dash;
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
        player.anim.SetFloat("yVelocity", rb.velocity.y);
        CheckForDashInput();
    }

    public virtual void PhysicUpdate()
    {
        
    }

    private void CheckForDashInput()
    {
        if (player.IsWallDetected() || player.dashTrigger)
            return;

        if (Dash)
        {
            stateMachine.SwitchState(typeof(PlayerDashState));
        }

    }
  
}
