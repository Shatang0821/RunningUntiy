using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StageJudge : MonoBehaviour
{
    [SerializeField] private int stageIndex;
    private Transform respawnPos;
    private void Awake()
    {
        respawnPos = transform.Find("Respawn Pos");
        if (respawnPos != null)
        {
            //Debug.Log("Respawn Position: " + respawnPos.position.ToString());
        }
        else
        {
            Debug.LogError("Respawn Pos not found!"); // ���X�|�[���ʒu��������Ȃ��ꍇ�̃G���[���b�Z�[�W
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("On");
        if(collision.gameObject.tag == "Player")
        {
            CameraController.Instance.ChangeCameraPos(this.transform);
            PlayerGenerator.Instance.SetSpawnPos(respawnPos);
            SetCurrentStage();
        }
    }

    private void SetCurrentStage()
    {
        StageManager.Instance.UpdateStageIndex(stageIndex);
    }
}