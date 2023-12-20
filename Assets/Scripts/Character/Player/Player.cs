using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Player : Entity
{
    [Header("==== PARTICLE ====")]
    public ParticleSystem movementParticle;
    public ParticleSystem fallParticle;
    public ParticleSystem jumpParticle;
    public ParticleSystem touchParticle;
    [Space]
    public float jumpInputBufferTime = 0.5f;

    [HideInInspector]
    public WaitForSeconds waitJumpInputBufferTime;

    //ジャンプ関連 キーを押したらtrueになる
    [HideInInspector]
    public bool HasJumpInputBuffer{ get; set; }

    //ダッシュ影
    public GameObject dashGhost;

    [HideInInspector]
    public Sprite sprite => gameObject.GetComponentInChildren<SpriteRenderer>().sprite;

    [Header("==== DEATH ====")]
    public GameObject DeathVFX;


    protected override void Awake()
    {
        base.Awake();
        waitJumpInputBufferTime = new WaitForSeconds(jumpInputBufferTime);
    }

    private void OnDisable()
    {
        HasJumpInputBuffer = false;
    }

    public void SetJumpInputBufferTimer()
    {
        StopCoroutine(nameof(JumpInputBufferCoroutine));
        StartCoroutine(nameof(JumpInputBufferCoroutine));
    }

    IEnumerator JumpInputBufferCoroutine()
    {
        HasJumpInputBuffer = true;

        yield return waitJumpInputBufferTime;

        HasJumpInputBuffer = false;
    }

    #region 死亡処理

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Traps")
        {
            Die();
            Debug.Log("Die");
        }
    }

    void Die()
    {
        GameManager.GameState = GameState.Respawn;
        PoolManager.Release(DeathVFX, transform.position);
        this.gameObject.SetActive(false);

        EventCenter.TriggerEvent(EventNames.Respawn);
    }

    #endregion
}
