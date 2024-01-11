using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private List<GameObject>stages = new List<GameObject>();

    private GameObject currentStage;
    private int currentIndex = 0;
    [SerializeField] bool isUseManager;

    private void Start()
    {
        if(isUseManager)
            Initialize();
    }

    private void Initialize()
    {
        SetStages();
        
    }

    private void Update()
    {

    }


    /// <summary>
    /// 子オブジェクトにあるステージをリストに追加
    /// </summary>
    void SetStages()
    {
        foreach(Transform child in transform) 
        { 
            stages.Add(child.gameObject);
            //child.gameObject.SetActive(false);
        }
        currentStage = stages[currentIndex];
    }


}
