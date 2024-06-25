using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// エリア到達処理
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
            //エリアに入ってから実行する処理
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
