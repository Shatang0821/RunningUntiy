using System.Collections;
using UnityEngine;

public class Player : Entity
{
    [HideInInspector]
    public PlayerAction PlayerAction;                           //アクション制御コンポーネント
    [HideInInspector]
    public PlayerParticleController PlayerParticleController;   //パーティクル制御コンポーネント
    [HideInInspector]
    public PlayerAudioController PlayerAudioController;         //効果音制御コンポーネント

    [Header("Collision info")]
    public Transform enemyCheck;
    public Vector2 attackDistance;
    public LayerMask whatIsEnemy;
    
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
        if (collision.collider.CompareTag("Traps") && !isDeaded) Die();
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
