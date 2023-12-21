using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    [SerializeField] GameObject[] cameraPos;

    private Camera mainCamera; // ���C���J�����̎Q�Ƃ��i�[

    protected override void Awake()
    {
        base.Awake();
        mainCamera = Camera.main;
    }

    public void ChangeCamera(int index)
    {
        //Debug.Log("Change");
        mainCamera.transform.position = cameraPos[index].transform.position;
    }
}
