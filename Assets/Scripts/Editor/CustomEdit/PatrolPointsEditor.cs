using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(PatrolPointsMover))]
public class PatrolPointsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // 既存のインスペクターUIを描画

        PatrolPointsMover script = (PatrolPointsMover)target;

        if (GUILayout.Button("新しい通過点を追加"))
        {
            // 連番を決定するために既存のポイントの数を取得
            GameObject newPoint = new GameObject("PatrolPoint" + (script.points.Count + 1));

            // 新しいポイントの親を、スクリプトがアタッチされているオブジェクトの親に設定
            newPoint.transform.parent = script.transform.parent;
            // 新しいポイントの位置を適切に設定
            newPoint.transform.position = Vector3.zero;

            // リストに新しいポイントを追加
            script.points.Add(newPoint.transform);

            // エディタ上での変更を保存
            EditorUtility.SetDirty(script);
        }
    }
}
