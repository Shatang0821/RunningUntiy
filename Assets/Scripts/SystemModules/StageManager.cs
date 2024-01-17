using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    [SerializeField] private List<GameObject>stages = new List<GameObject>();   //ステージを保つリスト

    private int currentIndex = 0;               //現在のステージ

    /// <summary>
    /// StageManagerの初期化
    /// </summary>
    public IEnumerator Initialize()
    {
        yield return StartCoroutine(nameof(SetStages));
        Debug.Log("Stage初期化完了");
    }

    /// <summary>
    /// 子オブジェクトにあるステージをリストに追加
    /// </summary>
    private IEnumerator SetStages()
    {
        foreach(Transform child in transform) 
        { 
            stages.Add(child.gameObject);
            child.gameObject.SetActive(false);
            yield return null;
        }
        //アクティブ化にするステージを更新する
        UpdateStageIndex(currentIndex);
    }

    /// <summary>
    /// アクティブ化にするステージの更新
    /// </summary>
    /// <param name="index">現在のステージ番号</param>
    public void UpdateStageIndex(int index)
    {
        currentIndex = index;

        // 2個前のステージを非アクティブ化
        if (currentIndex > 1)
        {
            stages[currentIndex - 2].SetActive(false);
        }

        // 現在のステージをアクティブ化
        stages[currentIndex].SetActive(true);

        // 後ろのステージをアクティブ化（もし存在すれば）
        if (currentIndex < stages.Count - 1)
        {
            stages[currentIndex + 1].SetActive(true);
        }
    }

}
