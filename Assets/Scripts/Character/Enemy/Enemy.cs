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
    // ������̗�
    [SerializeField] private float forceMagnitude = 5f; // �͂̒���
    [SerializeField] private float gravityScale;

    float randomAngle;//���ˊp

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
        //�d�͂̏�����
        rb.gravityScale = 1;
        //�R���C�_�[�L����
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
    /// �G��������ɔ��˂��A�R���C�_�[�𖳌��ɂ��郁�\�b�h�B
    /// </summary>
    /// <param name="forceMagnitude">���˂Ɏg�p�����͂̑傫���B</param>
    public void LaunchAndDisable()
    {
        rb.gravityScale = gravityScale;
        //���ˊp�����߂�
        randomAngle = Random.Range(30f, 150f);

        Vector2 launchDirection = new(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad));

        rb.AddForce(launchDirection * forceMagnitude, ForceMode2D.Impulse);
        // �R���C�_�[�𖳌���
        cd.enabled = false;
    }

    public override void Die()
    {
        deathTrigger = true;
    }

    /// <summary>
    /// �A�j���[�V�������I�����ɌĂяo��
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
