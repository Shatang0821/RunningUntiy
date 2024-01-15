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
            Debug.Log("Respawn Position: " + respawnPos.position.ToString());
        }
        else
        {
            Debug.LogError("Respawn Pos not found!"); // リスポーン位置が見つからない場合のエラーメッセージ
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("On");

        CameraController.Instance.ChangeCameraPos(this.transform);
        PlayerGenerator.Instance.SetSpawnPos(respawnPos);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //CameraController.Instance.ChangeCameraPos(this.transform.position);
    }
}
