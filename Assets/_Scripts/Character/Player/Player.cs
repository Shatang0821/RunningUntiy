using System.Collections;
using UnityEngine;

public class Player : Entity
{
    public PlayerJump PlayerJump;

    [Header("==== PARTICLE ====")]
    public ParticleSystem movementParticle;
    public ParticleSystem fallParticle;
    public ParticleSystem jumpParticle;
    public ParticleSystem touchParticle;

    [Header("Collision info")]
    [SerializeField] public Transform enemyCheck;
    [SerializeField] public Vector2 attackDistance;
    [SerializeField] public LayerMask whatIsEnemy;

    [Header("Audio info")]
    [SerializeField] public AudioData jumpSFX;
    [SerializeField] public AudioData dashSFX;
    [SerializeField] public AudioData hitSFX;



    public GameObject dashGhost;
    public bool dashItemGet = false;
    [HideInInspector]
    public Sprite sprite => gameObject.GetComponentInChildren<SpriteRenderer>().sprite;
    private void Initialize()
    {
        PlayerJump = GetComponent<PlayerJump>();
    }
    // ライフサイクルメソッド
    protected override void Awake()
    {
        base.Awake();
        Initialize();
        PlayerJump.Initialize();
    }

    private void OnEnable()
    {
        isDeaded = false;
    }

    // 衝突処理
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireCube(enemyCheck.position, attackDistance);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Traps" && !isDeaded) Die();
        if (collision.collider.tag == "Enemy" && !isDeaded) Die();
    }

    public override void Die()
    {
        base.Die();
        AudioManager.Instance.PlaySFX(hitSFX);
        CameraController.Instance.CameraShake(0.06f, 0.1f);
        GameManager.GameState = GameState.Respawn;
        EventCenter.TriggerEvent(StateEvents.SpawnPlayer);
        isDeaded = true;
    }
}
