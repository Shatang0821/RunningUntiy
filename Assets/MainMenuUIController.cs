using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
public class MainMenuUIController : MonoBehaviour
{
    [Header("==== CANVAS ====")]
    [SerializeField] Canvas mainMenuCanvas;

    [Header("==== BUTTONS ====")]
    [SerializeField] Button buttonStart;
    [SerializeField] Button buttonOptions;
    [SerializeField] Button buttonQuit;

    private void OnEnable()
    {
        EventCenter.Subscribe(ButtonNames.startButton, OnStartButtonClicked);
        EventCenter.Subscribe(ButtonNames.optionButton, OnOptionButtonClicked);
        EventCenter.Subscribe(ButtonNames.quitButton, OnQuitButtonClicked);

    }

    private void OnDisable()
    {
        EventCenter.Unsubscribe(ButtonNames.startButton, OnStartButtonClicked);
        EventCenter.Unsubscribe(ButtonNames.optionButton, OnOptionButtonClicked);
        EventCenter.Unsubscribe(ButtonNames.quitButton, OnQuitButtonClicked);
    }

    private void Start()
    {
        GameManager.GameState = GameState.MainMenu;
        UIInput.Instance.SelectUI(buttonStart);
    }

    /// <summary>
    /// メニュー画面を消して、ゲームシーンに遷移
    /// </summary>
    private void OnStartButtonClicked()
    {
        SceneLoader.Instance.LoadGamePlayScene();
    }

    private void OnOptionButtonClicked()
    {
        //UIInput.Instance.SelectUI(buttonOptions);
    }

    /// <summary>
    /// ゲームを終了
    /// </summary>
    private void OnQuitButtonClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
