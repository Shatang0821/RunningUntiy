using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageJudge : MonoBehaviour
{
    [SerializeField] int stageIndex;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CameraController.Instance.ChangeCamera(stageIndex);
        PlayerGenerator.Instance.SetSpawnPos(stageIndex);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
}
