using System.Collections;
using UnityEngine;

public class Player : Entity
{
    [HideInInspector]
    public PlayerAction PlayerAction;
    [HideInInspector]
    public PlayerParticleController PlayerParticleController;
    [HideInInspector]
    public PlayerAudioController PlayerAudioController;

    [Header("Collision info")]
    public Transform enemyCheck;
    public Vector2 attackDistance;
    public LayerMask whatIsEnemy;

    //public GameObject dashGhost;
    //public bool dashItemGet = false;
    //[HideInInspector]
    //public Sprite sprite => gameObject.GetComponentInChildren<SpriteRenderer>().sprite;
    private void Initialize()
    {
        PlayerAction = GetComponent<PlayerAction>();
        PlayerParticleController = GetComponent<PlayerParticleController>();
        PlayerAudioController = GetComponent<PlayerAudioController>();
    }
    // ライフサイクルメソッド
    protected override void Awake()
    {
        base.Awake();
        Initialize();
        PlayerAction.Initialize();
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
        AudioManager.Instance.PlaySFX(PlayerAudioController.hitSFX);
        CameraController.Instance.CameraShake(0.06f, 0.1f);
        GameManager.GameState = GameState.Respawn;
        EventCenter.TriggerEvent(StateEvents.SpawnPlayer);
        isDeaded = true;
    }
}
