using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region Components
    public Animator anim { get; private set; }

    public Rigidbody2D rb { get; private set; }

    #endregion

    [Header("Collision info")]
    [SerializeField] protected Transform groundCheck;         //�n�ʃ`�F�b�N
    [SerializeField] protected float groundCheckDistance;     //�`�F�b�N����
    [SerializeField] protected Transform wallCheck;           //�ǃ`�F�b�N
    [SerializeField] protected float wallCheckDistance;       //�`�F�b�N����
    [SerializeField] protected LayerMask whatIsGround;        //���C���[�ݒ�

    public int facingDir { get; private set; } = 1;
    protected bool facingRight = true;

    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {   

    }

    #region Velocity
    public void ZeroVelocity() => rb.velocity = Vector2.zero;

    public void SetVelocity(Vector2 velocity,float _xDir)
    {
        rb.velocity = velocity;
        FlipController(_xDir);
    }

    /// <summary>
    /// X���x�ݒ�
    /// </summary>
    /// <param name="velocityX">X���x</param>
    public void SetVelocityX(float velocityX, float _xDir)
    {
        rb.velocity = new Vector2(velocityX, rb.velocity.y);
        FlipController(_xDir);
    }

    /// <summary>
    /// Y���x�ݒ�
    /// </summary>
    /// <param name="velocityY">Y���x</param>
    public void SetVelocityY(float velocityY)
    {
        rb.velocity = new Vector2(rb.velocity.x, velocityY);
    }

    public void SetUseGravity(float value)
    {
        rb.gravityScale = value;
    }
    #endregion

    #region Collision
    /// <summary>
    /// �n�ʃ`�F�b�N
    /// </summary>
    /// <returns>true or false</returns>
    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }
    #endregion

    #region Flip
    /// <summary>
    /// �v���C���[���]�֐�
    /// </summary>
    public virtual void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        //y�����S��180��]
        transform.Rotate(0, 180, 0);
    }

    /// <summary>
    /// ���]���f����֐�
    /// </summary>
    /// <param name="_x"></param>
    public virtual void FlipController(float _x)
    {
        if (_x > 0 && !facingRight)
            Flip();
        else if (_x < 0 && facingRight)
            Flip();

    }
    #endregion
}