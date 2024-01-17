using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(PatrolPointsMover))]
public class PatrolPointsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // �����̃C���X�y�N�^�[UI��`��

        PatrolPointsMover script = (PatrolPointsMover)target;

        if (GUILayout.Button("�V�����ʉߓ_��ǉ�"))
        {
            // �A�Ԃ����肷�邽�߂Ɋ����̃|�C���g�̐����擾
            GameObject newPoint = new GameObject("PatrolPoint" + (script.points.Count + 1));

            // �V�����|�C���g�̐e���A�X�N���v�g���A�^�b�`����Ă���I�u�W�F�N�g�̐e�ɐݒ�
            newPoint.transform.parent = script.transform.parent;
            // �V�����|�C���g�̈ʒu��K�؂ɐݒ�
            newPoint.transform.position = Vector3.zero;

            // ���X�g�ɐV�����|�C���g��ǉ�
            script.points.Add(newPoint.transform);

            // �G�f�B�^��ł̕ύX��ۑ�
            EditorUtility.SetDirty(script);
        }
    }
}
