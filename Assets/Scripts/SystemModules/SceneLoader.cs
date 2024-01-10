using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : PersistentSingleton<SceneLoader>
{
    const string GAMESCENE = "GameScene";
    const string MAIN_MENU = "TitleScene";

    void Load(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator LoadingCoroutine(string sceneName)
    {
        //シーンのロードを完了しているかチェック
        var loadingOperation = SceneManager.LoadSceneAsync(sceneName);
        loadingOperation.allowSceneActivation = false;  //ロード完了のシーンのアクティブ状態切り替え

        //シーンのロードが完全に終わるまで待つ
        yield return new WaitUntil(() => loadingOperation.progress >= 0.90f);

        loadingOperation.allowSceneActivation = true;

        // シーンのアクティブを待つ
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == sceneName);


        GameManager.GameState = GameState.Initialize;
    }

    public void LoadGamePlayScene()
    {
        StopAllCoroutines();    
        StartCoroutine(LoadingCoroutine(GAMESCENE));
    }

    public void LoadMainMenuScene()
    {
        StopAllCoroutines();
        StartCoroutine(LoadingCoroutine(MAIN_MENU));
    }

}
