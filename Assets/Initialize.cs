using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialize : MonoBehaviour
{
    IEnumerator Start()
    {

        //ゲームが初期状態に入るまでを待つ
        yield return GameManager.GameState == GameState.Initialize;

        //初期化に入ると入力を無効化にする
        EventCenter.TriggerEvent(InputEvents.disableAllInput);

        //ステージの初期化が終わるまでを待つ
        yield return StageManager.Instance.Initialize();
        Debug.Log("InitilizeObject初期化完了");

        // ゲーム状態を更新する
        GameManager.GameState = GameState.Respawn;
        Debug.Log(GameManager.GameState);
    }
}
