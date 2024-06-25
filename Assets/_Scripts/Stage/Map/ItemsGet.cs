using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// アイテム取得メソッド
/// </summary>
public class ItemsGet : MonoBehaviour
{
    [SerializeField] GameObject collectVFX;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
        }
    }

}
