using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class PoolManager : MonoBehaviour
{

    [SerializeField] Pool[] dashGhost;
    [SerializeField] Pool[] apple;

    // 各プレハブとそれに関連するオブジェクトプールを関連付けるための辞書。
    static Dictionary<GameObject, Pool> dictionary;

    private void Awake()
    {
        dictionary = new Dictionary<GameObject, Pool>();
        Initialize(dashGhost);
        Initialize(apple);
    }

    // UNITY_EDITORディレクティブは、Unityエディタ環境内でのみコードを実行するためのもの。
    // 実際のゲームプレイでは実行されない。
#if UNITY_EDITOR
    void OnDestroy()
    {
        //プールサイズが正しいかをチェックする
        CheckPoolSize(dashGhost);
        CheckPoolSize(apple);
    }
#endif

    // 各プールのランタイム時のサイズを確認し、初期設定よりも大きい場合は警告を表示する。
    void CheckPoolSize(Pool[] pools)
    {
        foreach (var pool in pools)
        {
            if (pool.RuntimeSize > pool.Size)
            {
                Debug.LogWarning(
                    string.Format("プール:{0}は初期サイズ{2}より{1}の方が正しい!",
                    pool.Prefab.name,
                    pool.RuntimeSize,
                    pool.Size));
            }
        }
    }


    // プールを初期化し、それぞれのプールを辞書に追加する。
    void Initialize(Pool[] pools)
    {
        foreach (var pool in pools)
        {
#if UNITY_EDITOR    
            if (dictionary.ContainsKey(pool.Prefab))
            {
                Debug.LogError("複数定義しているプレハブがいます prefab:" + pool.Prefab.name);
                continue;
            }
#endif
            dictionary.Add(pool.Prefab, pool);

            // Hierarchyビューで見やすくするために新しいGameObjectを作成して、その子としてプールオブジェクトを持つ。
            Transform poolParent = new GameObject("Pool:" + pool.Prefab.name).transform;
            poolParent.parent = transform;

            pool.Initialize(poolParent);
        }
    }

    /// <summary>
    /// <para>プール内に指定された<paramref name="prefab"></paramref>をゲームオブジェクトに返す。</para>
    /// </summary>
    /// <param name="prefab">
    /// <para>指定されたプレハブ</para>
    /// </param>
    /// <param name="position">
    /// <para>指定された生成位置</para>
    /// </param>
    /// <returns></returns>
    public static GameObject Release(GameObject prefab, Vector3 position)
    {
#if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("pool Manager could NOT find prefab : " + prefab.name);

            return null;
        }
#endif
        return dictionary[prefab].PreparedObject(position);
    }

    /// <summary>
    /// <para>プール内に指定された<paramref name="prefab"></paramref>をゲームオブジェクトに返す。</para>
    /// </summary>
    /// <param name="prefab">
    /// <para>指定されたプレハブ</para>
    /// </param>
    /// <param name="position">
    /// <para>指定された生成位置</para>
    /// </param>
    /// <returns></returns>
    public static GameObject Release(GameObject prefab, Vector3 position,Quaternion rotation,Sprite sprite,float alpha)
    {
#if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("pool Manager could NOT find prefab : " + prefab.name);

            return null;
        }
#endif
        return dictionary[prefab].PreparedObject(position,rotation,sprite,alpha);
    }
}
