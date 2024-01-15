using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialize : MonoBehaviour
{
    IEnumerator Start()
    {

        //�Q�[����������Ԃɓ���܂ł�҂�
        yield return GameManager.GameState == GameState.Initialize;

        //�������ɓ���Ɠ��͂𖳌����ɂ���
        EventCenter.TriggerEvent(InputEvents.disableAllInput);

        //�X�e�[�W�̏��������I���܂ł�҂�
        yield return StageManager.Instance.Initialize();
        Debug.Log("InitilizeObject����������");

        // �Q�[����Ԃ��X�V����
        GameManager.GameState = GameState.Respawn;
        Debug.Log(GameManager.GameState);
    }
}
