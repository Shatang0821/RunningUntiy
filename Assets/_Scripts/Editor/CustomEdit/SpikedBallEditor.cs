using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpikedBall))]
public class SpikedBallEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SpikedBall script = (SpikedBall)target;

        if (GUILayout.Button("Add Chain"))
        {
            script.AddChain();
        }

        // エディタ上での変更を保存
        EditorUtility.SetDirty(script);
    }
}
