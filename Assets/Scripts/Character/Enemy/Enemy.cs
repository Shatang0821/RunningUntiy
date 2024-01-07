using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    //[SerializeField] protected LayerMask whatIsPlayer;

    //[Header("Attack info")]
    //public float attackDistance;

    [Header("Move info")]
    public float moveSpeed;
    public float idleTime;

    [Header("Death info")]
    public bool deathTrigger;
    // 上向きの力
    [SerializeField] private float forceMagnitude = 5f; // 力の調整
    [SerializeField] private float gravityScale;

    float randomAngle;//発射角

    public EnemyStateMachine stateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
    }

    protected virtual void OnEnable()
    {
        Initialize();
        deathTrigger = false;
    }

    void Initialize()
    {
        //重力の初期化
        rb.gravityScale = 1;
        //コライダー有効化
        cd.enabled = true;
    }

    private void Update()
    {
        stateMachine.currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.PhysicUpdate();
    }

    /// <summary>
    /// 敵を上方向に発射し、コライダーを無効にするメソッド。
    /// </summary>
    /// <param name="forceMagnitude">発射に使用される力の大きさ。</param>
    public void LaunchAndDisable()
    {
        rb.gravityScale = gravityScale;
        //発射角を決める
        randomAngle = Random.Range(30f, 150f);

        Vector2 launchDirection = new(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad));

        rb.AddForce(launchDirection * forceMagnitude, ForceMode2D.Impulse);
        // コライダーを無効化
        cd.enabled = false;
    }

    public override void Die()
    {
        deathTrigger = true;
    }

    /// <summary>
    /// アニメーションを終了時に呼び出す
    /// </summary>
    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();


    //public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, 50, whatIsPlayer);

    //protected override void OnDrawGizmos()
    //{
    //    base.OnDrawGizmos();

    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * facingDir, transform.position.y));
    //}
}
