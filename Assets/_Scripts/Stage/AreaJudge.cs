using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// �G���A���B����
/// </summary>
public class AreaJudge : MonoBehaviour
{
    [SerializeField] private int stageIndex;
    private Transform respawnPos;
    private void Awake()
    {
        respawnPos = transform.Find("Respawn Pos");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //�G���A�ɓ����Ă�����s���鏈��
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
