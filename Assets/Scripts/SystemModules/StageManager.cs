using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    [SerializeField] private List<GameObject>stages = new List<GameObject>();

    private GameObject currentStage;

    private int currentIndex = 0;

    [SerializeField] bool isUseManager;

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
            //child.gameObject.SetActive(false);
            yield return null;
        }
        currentStage = stages[currentIndex];
    }


}
