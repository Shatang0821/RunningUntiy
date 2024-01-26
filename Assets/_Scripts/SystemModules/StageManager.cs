using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    [SerializeField] private List<GameObject>stages = new List<GameObject>();   //�X�e�[�W��ۂ��X�g

    private int currentIndex = 0;               //���݂̃X�e�[�W�X�e�[�W
    private int previousIndex = -1;             // �ȑO�̃X�e�[�W�C���f�b�N�X
    /// <summary>
    /// StageManager�̏�����
    /// </summary>
    public IEnumerator Initialize()
    {
        yield return StartCoroutine(nameof(SetStages));
        Debug.Log("Stage����������");
    }

    /// <summary>
    /// �q�I�u�W�F�N�g�ɂ���X�e�[�W�����X�g�ɒǉ�
    /// </summary>
    private IEnumerator SetStages()
    {
        foreach(Transform child in transform) 
        { 
            stages.Add(child.gameObject);
            child.gameObject.SetActive(false);
            yield return null;
        }
        //�A�N�e�B�u���̕K�v�̂���X�e�[�W���X�V����
        UpdateStageIndex(currentIndex);
    }

    /// <summary>
    /// �A�N�e�B�u���ɂ���X�e�[�W�̍X�V
    /// </summary>
    /// <param name="index">���݂̃X�e�[�W�ԍ�</param>
    public void UpdateStageIndex(int index)
    {
        currentIndex = index;
        // �ȑO�̃X�e�[�W�Ƃ��̗אڂ���X�e�[�W���A�N�e�B�u��
        if (previousIndex != -1)
        {
            if ((previousIndex > 0) && (previousIndex - 1) != currentIndex)
                SetStageActive(previousIndex - 1, false);
            if ((previousIndex < stages.Count - 1) && ((previousIndex + 1) != currentIndex))
                SetStageActive(previousIndex + 1, false);
        }

        // ���݂̃X�e�[�W���A�N�e�B�u��
        SetStageActive(currentIndex, true);

        // �אڂ���X�e�[�W���A�N�e�B�u���i�������݂���΁j
        if (currentIndex > 0)
            SetStageActive(currentIndex - 1, true);
        if (currentIndex < stages.Count - 1)
            SetStageActive(currentIndex + 1, true);
        // ���݂̃C���f�b�N�X���ȑO�̃C���f�b�N�X�Ƃ��ĕۑ�
        previousIndex = currentIndex;
    }

    /// <summary>
    /// �X�e�[�W�̃A�N�e�B�u��Ԃ�ݒ肷��
    /// </summary>
    /// <param name="stageIndex">�X�e�[�W�ԍ�</param>
    /// <param name="isActive">�A�N�e�B�u���</param>
    private void SetStageActive(int stageIndex, bool isActive)
    {
        //���X�g���ɂ���X�e�[�W���`�F�b�N
        if (stageIndex >= 0 && stageIndex < stages.Count)
        {
            stages[stageIndex].SetActive(isActive);
        }
    }
}
