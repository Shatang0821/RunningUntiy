using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    [SerializeField] private List<GameObject>stages = new List<GameObject>();   //�X�e�[�W��ۂ��X�g

    private int currentIndex = 0;               //���݂̃X�e�[�W

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
        //�A�N�e�B�u���ɂ���X�e�[�W���X�V����
        UpdateStageIndex(currentIndex);
    }

    /// <summary>
    /// �A�N�e�B�u���ɂ���X�e�[�W�̍X�V
    /// </summary>
    /// <param name="index">���݂̃X�e�[�W�ԍ�</param>
    public void UpdateStageIndex(int index)
    {
        currentIndex = index;

        // 2�O�̃X�e�[�W���A�N�e�B�u��
        if (currentIndex > 1)
        {
            stages[currentIndex - 2].SetActive(false);
        }

        // ���݂̃X�e�[�W���A�N�e�B�u��
        stages[currentIndex].SetActive(true);

        // ���̃X�e�[�W���A�N�e�B�u���i�������݂���΁j
        if (currentIndex < stages.Count - 1)
        {
            stages[currentIndex + 1].SetActive(true);
        }
    }

}
