using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDeactivate : MonoBehaviour
{
    [SerializeField] bool autoStartCoroutine = true; // コルーチンを自動開始、デフォルトはtrue
    [SerializeField] bool destroyGameObject;
    [SerializeField] float lifetime = 3f;

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
