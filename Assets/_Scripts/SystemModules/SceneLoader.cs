using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : PersistentSingleton<SceneLoader>
{
    const string STAGE1 = "Stage1";
    const string MAIN_MENU = "TitleScene";
    const string TUTORIAL = "TutorialScene";
    const string SCENESELECT = "StageSelect";
    void Load(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator LoadingCoroutine(string sceneName)
    {
        //シーンのロードを完了しているかチェック
        var loadingOperation = SceneManager.LoadSceneAsync(sceneName);
        loadingOperation.allowSceneActivation = false;  //ロード完了のシーンのアクティブ状態切り替え

        //シーンのロードが90%に終わるまで待つ残り10%はアクティブ化
        yield return new WaitUntil(() => loadingOperation.progress >= 0.90f);

        //シーンをアクティブ化にする
        loadingOperation.allowSceneActivation = true;

        // シーンのアクティブ化(残りの10%)が終わるまでを待つ
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == sceneName);
        GameManager.GameState = GameState.Initialize;
    }

    public void LoadStage1Scene()
    {
        StopAllCoroutines();
        StartCoroutine(LoadingCoroutine(STAGE1));
    }

    public void LoadMainMenuScene()
    {
        StopAllCoroutines();
        StartCoroutine(LoadingCoroutine(MAIN_MENU));
    }

    public void LoadStageSelectScene()
    {
        StopAllCoroutines();
        StartCoroutine(LoadingCoroutine(SCENESELECT));
    }

    /// <summary>
    /// チュートリアルシーンに遷移
    /// </summary>
    public void LoadTutorialScene()
    {
        StopAllCoroutines();
        StartCoroutine(LoadingCoroutine(TUTORIAL));
    }
}
