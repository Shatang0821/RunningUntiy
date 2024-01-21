using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool
{
    // プレハブへの参照を外部から取得するためのプロパティ
    public GameObject Prefab => prefab;

    // サイズを外部から取得するためのプロパティ
    public int Size => size;

    // 実行時のキューのサイズを取得するためのプロパティ
    public int RuntimeSize => queue.Count;

    [SerializeField]
    private GameObject prefab; // このプールに格納するゲームオブジェクトのプレハブ

    [SerializeField]
    private int size = 1; // プールの初期サイズ

    // ゲームオブジェクトを保持するためのキュー
    private Queue<GameObject> queue;

    // ゲームオブジェクトがインスタンス化されるときの親オブジェクト
    private Transform parent;

    /// <summary>
    /// キューの初期化し、指定された数のゲームオブジェクトをキューに追加する
    /// </summary>
    public void Initialize(Transform parent)
    {
        queue = new Queue<GameObject>();
        this.parent = parent;

        for(var i = 0;i<size; i++)
        {
            queue.Enqueue(Copy());
        }
    }

    /// <summary>
    /// プレハブからゲームオブジェクトを作成し、非アクティブ状態にする
    /// </summary>
    private GameObject Copy()
    {
        var copy = GameObject.Instantiate(prefab,parent);
        copy.SetActive(false);
        return copy;
    }

    /// <summary>
    /// 利用可能なオブジェクトをキューから取得する。
    /// </summary>
    private GameObject AvailableObject()
    {
        GameObject avaliableObject = null;

        // キューから使えるオブジェクトあるなら
        if (queue.Count > 0 && !queue.Peek().activeSelf) 
        { 
            avaliableObject = queue.Dequeue();
        }
        else
        {
            //もう一度作る
            avaliableObject = Copy();
        }
        //取り出したオブジェクトを末尾に入れる
        queue.Enqueue(avaliableObject);

        return avaliableObject;
    }

    /// <summary>
    /// 利用可能なゲームオブジェクトを取得してアクティブ化する
    /// </summary>
    public GameObject PreparedObject()
    {
        GameObject preparedObject = AvailableObject();
        preparedObject.SetActive(true);
        return preparedObject;
    }

    /// <summary>
    /// 特定の位置を基に生成
    /// </summary>
    /// <param name="position">特定の位置</param>
    /// <returns></returns>
    public GameObject PreparedObject(Vector3 position)
    {
        GameObject preparedObject = AvailableObject();

        preparedObject.SetActive(true);
        preparedObject.transform.position = position;

        return preparedObject;
    }

    public GameObject PreparedObject(Vector3 position,Quaternion rotation,Sprite sprite,float alpha)
    {
        GameObject preparedObject = AvailableObject();
        SpriteRenderer spriteRenderer = preparedObject.GetComponent<SpriteRenderer>();
#if UNITY_EDITOR
        if(spriteRenderer != null)
        {
            spriteRenderer.sprite = sprite;
            Color color = spriteRenderer.color;
            color.a = alpha; // 设置透明度
            spriteRenderer.color = color;
        }
        else
        {
            Debug.LogWarning(preparedObject.name + "はspriteコンポーネントないです");
        }
#endif
        preparedObject.SetActive(true);
        preparedObject.transform.position = position;
        preparedObject.transform.rotation = rotation;

        return preparedObject;
    }
}
