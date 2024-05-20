using UnityEngine;

// プレイヤーの各状態の基底クラス
[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/PlayerState", fileName = "PlayerState")]
public class PlayerState : ScriptableObject, IState
{
    protected PlayerStateMachine stateMachine; // 状態マシン
    protected Player player;                  // プレイヤーの参照

    protected PlayerAction playerAction;      //行動関係クラス
    protected PlayerParticleController playerParticleController;
    protected PlayerAudioController playerAudioController;


    protected Rigidbody2D rb;                 // リジッドボディの参照

    #region Animator
    [SerializeField] private string animBoolName; // アニメーターのブール名
    protected int stateBoolHash;                  // アニメーターのハッシュ値
    #endregion

    #region State
    protected float stateTimer;             // 状態のタイマー

    protected bool triggerCalled;           // トリガーが呼ばれたかどうか
    
    // 重力の基準値
    protected const float GRAVITY_BASE = 0.1f;
    #endregion

    #region Input
    protected PlayerInput input;            // プレイヤーの入力

    protected Vector2 dirInput => input.Axis;

    // X軸の入力
    protected float xInput => input.Axis.x;
    // Y軸の入力
    protected float yInput => input.Axis.y;

    protected bool Jump => input.Jump;      // ジャンプ入力
    //protected bool StopJump => input.StopJump; // ジャンプ停止入力
    protected bool Dash => input.Dash;      // ダッシュ入力
    protected bool Climb => input.Climb;    // 登る入力
    #endregion

    private void OnEnable()
    {
        stateBoolHash = Animator.StringToHash(animBoolName); // アニメーターハッシュの初期化
    }

    // 初期化処理
    public void Initialize(Player _player, PlayerStateMachine _stateMachine, PlayerInput _input)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.input = _input;

        this.playerAction = _player.PlayerAction;
        this.playerParticleController = _player.PlayerParticleController;
        this.playerAudioController = _player.PlayerAudioController;
    }

    // 状態に入った時の処理
    public virtual void Enter()
    {
        player.anim.SetBool(stateBoolHash, true); // アニメーターの状態を更新
        rb = player.rb;                           // リジッドボディの参照を取得
        if (player.IsGroundDetected())
        {
            EventCenter.TriggerEvent(StateEvents.SetDashTrigger, true);
        }
//        Debug.Log(stateMachine.currentState);
    }

    // 状態から出る時の処理
    public virtual void Exit()
    {
        player.anim.SetBool(stateBoolHash, false); // アニメーターの状態を更新
        player.SetUseGravity(GRAVITY_BASE);
    }

    // ロジックアップデート（毎フレームの更新処理）
    public virtual void LogicUpdate()
    {
        // ダッシュ入力のチェック
        if (CheckForDashInput())
            return;
        stateTimer -= Time.deltaTime;             // 状態タイマーの更新
        player.anim.SetFloat("yVelocity", rb.velocity.y); // Y軸の速度をアニメーターに設定
    }

    // 物理アップデート（物理演算の更新処理）
    public virtual void PhysicUpdate()
    {
        // 物理演算に関する追加の処理（必要に応じて）
    }

    // ダッシュ入力のチェック処理
    bool CheckForDashInput()
    {
        if (!playerAction.dashTrigger)
            return false;

        if (Dash)
        {
            stateMachine.SwitchState(typeof(PlayerDashState)); // ダッシュ状態に切り替える
            return true;
        }
        return false;
    }


}