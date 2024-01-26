using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    [SerializeField] private List<GameObject>stages = new List<GameObject>();   //ステージを保つリスト

    private int currentIndex = 0;               //現在のステージステージ
    private int previousIndex = -1;             // 以前のステージインデックス
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
        //アクティブ化の必要のあるステージを更新する
        UpdateStageIndex(currentIndex);
    }

    /// <summary>
    /// アクティブ化にするステージの更新
    /// </summary>
    /// <param name="index">現在のステージ番号</param>
    public void UpdateStageIndex(int index)
    {
        currentIndex = index;
        // 以前のステージとその隣接するステージを非アクティブ化
        if (previousIndex != -1)
        {
            if ((previousIndex > 0) && (previousIndex - 1) != currentIndex)
                SetStageActive(previousIndex - 1, false);
            if ((previousIndex < stages.Count - 1) && ((previousIndex + 1) != currentIndex))
                SetStageActive(previousIndex + 1, false);
        }

        // 現在のステージをアクティブ化
        SetStageActive(currentIndex, true);

        // 隣接するステージをアクティブ化（もし存在すれば）
        if (currentIndex > 0)
            SetStageActive(currentIndex - 1, true);
        if (currentIndex < stages.Count - 1)
            SetStageActive(currentIndex + 1, true);
        // 現在のインデックスを以前のインデックスとして保存
        previousIndex = currentIndex;
    }

    /// <summary>
    /// ステージのアクティブ状態を設定する
    /// </summary>
    /// <param name="stageIndex">ステージ番号</param>
    /// <param name="isActive">アクティブ状態</param>
    private void SetStageActive(int stageIndex, bool isActive)
    {
        //リスト内にあるステージをチェック
        if (stageIndex >= 0 && stageIndex < stages.Count)
        {
            stages[stageIndex].SetActive(isActive);
        }
    }
}
