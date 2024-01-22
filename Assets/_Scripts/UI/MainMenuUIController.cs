using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
public class MainMenuUIController : MonoBehaviour
{
    [Header("==== CANVAS ====")]
    [SerializeField] Canvas mainMenuCanvas;

    [Header("==== PANEL ====")]
    [SerializeField] GameObject operationPanel;

    [Header("==== BUTTONS ====")]
    [SerializeField] Button buttonStart;
    [SerializeField] Button buttonOptions;
    [SerializeField] Button buttonQuit;

    private void OnEnable()
    {
        EventCenter.Subscribe(ButtonEvents.startButton, OnStartButtonClicked);
        EventCenter.Subscribe(ButtonEvents.optionButton, OnOptionButtonClicked);
        EventCenter.Subscribe(ButtonEvents.quitButton, OnQuitButtonClicked);

    }

    private void OnDisable()
    {
        EventCenter.Unsubscribe(ButtonEvents.startButton, OnStartButtonClicked);
        EventCenter.Unsubscribe(ButtonEvents.optionButton, OnOptionButtonClicked);
        EventCenter.Unsubscribe(ButtonEvents.quitButton, OnQuitButtonClicked);
    }

    private void Start()
    {
        GameManager.GameState = GameState.MainMenu;
        UIInput.Instance.SelectUI(buttonStart);
        operationPanel.SetActive(false);
    }

    /// <summary>
    /// メニュー画面を消して、ゲームシーンに遷移
    /// </summary>
    private void OnStartButtonClicked()
    {
        SceneLoader.Instance.LoadStageSelectScene();
    }

    private void OnOptionButtonClicked()
    {
        //UIInput.Instance.SelectUI(buttonOptions);
        operationPanel.SetActive(true);
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
