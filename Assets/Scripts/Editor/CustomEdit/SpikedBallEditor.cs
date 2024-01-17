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

        // �G�f�B�^��ł̕ύX��ۑ�
        EditorUtility.SetDirty(script);
    }
}
