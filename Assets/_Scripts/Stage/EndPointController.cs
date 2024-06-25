    using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndPointController : MonoBehaviour
{
    [SerializeField] GameObject collectVFX; //エフェクト

    Transform player;       //プレイヤー位置
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //プレイヤーに触れると
        if(collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject.GetComponent<Transform>();
            StartCoroutine(nameof(WaitForAnimation));
            animator.SetTrigger("Get");
        }
    }

    IEnumerator WaitForAnimation()
    {
        //アニメーションを終わるのを待つ
        yield return new WaitForSeconds(1.5f);
        PoolManager.Release(collectVFX, transform.position);
        
        yield return new WaitForSeconds(0.5f);
        
        GameClear();
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// クリア処理
    /// </summary>
    private void GameClear()
    {
        EventCenter.TriggerEvent(TimeEvents.StartTime);
        EventCenter.TriggerEvent(InputEvents.FixedInput);
        //クリアしたらステージ選ぶシーンに戻る
        SceneLoader.Instance.LoadStageSelectScene();
    }
}
