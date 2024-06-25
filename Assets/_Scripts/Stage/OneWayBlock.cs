using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayBlock : MonoBehaviour
{
    Collider2D childCollider;
    [Header("Jump Info")]
    [SerializeField] private float jumpForce;       //片方移動可能プラットフォーム
    [SerializeField] private float waitSeconds;     //プレイヤーがすり抜けまでの時間
    WaitForSeconds waitTime;
    private void Awake()
    {
        waitTime = new WaitForSeconds(waitSeconds);
        childCollider = transform.GetChild(0).GetComponent<Collider2D>();
        if (childCollider == null)
        {
            Debug.LogWarning("not found collider2D");
        }
        else
        {
            childCollider.enabled = true;
        }
    }

    private void OnEnable()
    {
        childCollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //Playerの状態をカスタムジャンプに強制遷移させる
            childCollider.enabled = false;
            PlayerStateMachine playerStateMachine = collision.gameObject.GetComponent<PlayerStateMachine>();
            if(playerStateMachine != null )
            {
                playerStateMachine.ForceJumpStateChange(jumpForce);
            }
            if(childCollider!=null)
                childCollider.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && childCollider!= null)
        {
            StartCoroutine(EnableCollider(childCollider));
        }
    }

    /// <summary>
    /// 当たり判定を一定時間経てから有効にする
    /// </summary>
    /// <param name="collision">有効にするコライダー</param>
    /// <returns></returns>
    IEnumerator EnableCollider(Collider2D collision)
    { 
        yield return waitTime;
        collision.enabled = true;
    }
}
