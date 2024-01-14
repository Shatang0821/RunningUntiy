using System.Collections;
using UnityEngine;

public class Player : Entity
{
    [Header("==== PARTICLE ====")]
    public ParticleSystem movementParticle;  // 移動時のパーティクル
    public ParticleSystem fallParticle;      // 落下時のパーティクル
    public ParticleSystem jumpParticle;      // ジャンプ時のパーティクル
    public ParticleSystem touchParticle;     // 接触時のパーティクル
    [Header("Collision info")]
    [SerializeField] public Transform enemyCheck;         //地面チェック
    [SerializeField] public Vector2 attackDistance;          //攻撃できる距離
    [SerializeField] public LayerMask whatIsEnemy;        //レイヤー設定

    [Space]
    public float jumpInputBufferTime = 0.5f; // ジャンプ入力のバッファ時間

    [HideInInspector]
    public WaitForSeconds waitJumpInputBufferTime; // ジャンプ入力バッファ用の待機時間

    // ジャンプ関連。キーを押すとtrueになる
    [HideInInspector]
    public bool HasJumpInputBuffer { get; set; }

    // ダッシュ影
    public GameObject dashGhost;

    [HideInInspector]
    public Sprite sprite => gameObject.GetComponentInChildren<SpriteRenderer>().sprite; // 現在のスプライト

    // GUIの表示（デバッグ用）
    //void OnGUI()
    //{
    //    Rect rect = new Rect(200, 150, 200, 200);
    //    string message = facingDir.ToString();
    //    GUIStyle style = new GUIStyle();
    //    style.fontSize = 50;
    //    style.fontStyle = FontStyle.Bold;
    //    GUI.Label(rect, message, style);
    //}

    // 起動時の初期化処理
    protected override void Awake()
    {
        base.Awake();
        waitJumpInputBufferTime = new WaitForSeconds(jumpInputBufferTime);
    }

    // 有効化時の処理
    private void OnEnable()
    {
        isDeaded = false;
    }

    // 無効化時の処理
    private void OnDisable()
    {
        HasJumpInputBuffer = false;
    }

    // ジャンプ入力バッファのタイマーを設定する処理
    public void SetJumpInputBufferTimer()
    {
        StopCoroutine(nameof(JumpInputBufferCoroutine));
        StartCoroutine(nameof(JumpInputBufferCoroutine));
    }

    // ジャンプ入力バッファのコルーチン
    IEnumerator JumpInputBufferCoroutine()
    {
        HasJumpInputBuffer = true;

        yield return waitJumpInputBufferTime;

        HasJumpInputBuffer = false;
    }

    #region 衝突処理


    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireCube(enemyCheck.position, attackDistance);
    }

    // 衝突時の処理
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Traps" && !isDeaded) Die();
        if (collision.collider.tag == "Enemy" && !isDeaded) Die();
    }

    // 死亡時の処理
    public override void Die()
    {
        base.Die();
        // カメラを揺らす
        CameraController.Instance.CameraShake(0.06f, 0.1f);

        GameManager.GameState = GameState.Respawn;
        EventCenter.TriggerEvent(EventNames.SpawnPlayer);
        isDeaded = true;
        //Debug.Log("Die");
    }



    #endregion


}
