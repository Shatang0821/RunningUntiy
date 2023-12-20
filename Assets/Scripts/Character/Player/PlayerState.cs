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

    //Dash制限
    protected static bool dashTrigger;

    #endregion

    #region Input
    protected PlayerInput input;
    protected float xInput
    {
        get
        {
            if (input.Axis.x > 0)
                return 1;
            else if (input.Axis.x < 0)
                return -1;
            else
                return 0;
        }
    }
    protected float yInput
    {
        get
        {
            if (input.Axis.y > 0)
                return 1;
            else if (input.Axis.y < 0)
                return -1;
            else
                return 0;
        }
    }

    protected bool Jump => input.Jump;
    protected bool Dash => input.Dash;
    protected bool StopJump => input.StopJump;
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
        //wallJumpこれを使わない
    }

    private void CheckForDashInput()
    {
        if (dashTrigger)
            return;

        if (Dash)
        {
            stateMachine.SwitchState(typeof(PlayerDashState));
        }

    }
  
}
