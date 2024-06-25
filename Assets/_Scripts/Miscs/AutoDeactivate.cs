using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 自動削除(非アクティブ)制御クラス
/// </summary>
public class AutoDeactivate : MonoBehaviour
{
    [SerializeField] bool autoStartCoroutine = true; // コルーチンを自動開始、デフォルトはtrue
    [SerializeField] bool destroyGameObject;         // trueは削除falseは非アクティブ
    [SerializeField] float lifetime = 3f;            // オブジェクトの生存時間

    WaitForSeconds waitLifetime;

    void Awake()
    {
        waitLifetime = new WaitForSeconds(lifetime);
    }

    void OnEnable()
    {
        if (autoStartCoroutine)
        {
            StartCoroutine(DeactivateCoroutine());
        }
    }

    // 外部から自動的にコルーチンを開始するかどうかを制御するための新しい公開メソッド
    public void StartDeactivateCoroutine()
    {
        StartCoroutine(DeactivateCoroutine());
    }

    IEnumerator DeactivateCoroutine()
    {
        yield return waitLifetime;

        if (destroyGameObject)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    
}
