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

    [SerializeField] public float jumpInputBufferTime = 0.5f;

    public WaitForSeconds waitJumpInputBufferTime;

    //ジャンプ関連 キーを押したらtrueになる
    public bool HasJumpInputBuffer { get; set; }

    public GameObject dashGhost;

    public Sprite sprite => gameObject.GetComponentInChildren<SpriteRenderer>().sprite;
    

    protected override void Awake()
    {
        base.Awake();
        waitJumpInputBufferTime = new WaitForSeconds(jumpInputBufferTime);
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
}
