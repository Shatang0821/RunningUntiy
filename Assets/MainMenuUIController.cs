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
        EventCenter.Subscribe(ButtonNames.buttonStart, OnButtonStartClicked);
        EventCenter.Subscribe(ButtonNames.buttonOptions, OnButtonOptionsClicked);
        EventCenter.Subscribe(ButtonNames.buttonQuit, OnButtonQuitClicked);

    }

    private void Start()
    {
        GameManager.GameState = GameState.MainMenu;
        UIInput.Instance.SelectUI(buttonStart);
    }

    /// <summary>
    /// メニュー画面を消して、ゲームシーンに遷移
    /// </summary>
    void OnButtonStartClicked()
    {
        SceneLoader.Instance.LoadGamePlayScene();
    }

    void OnButtonOptionsClicked()
    {
        //UIInput.Instance.SelectUI(buttonOptions);
    }

    /// <summary>
    /// ゲームを終了
    /// </summary>
    void OnButtonQuitClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
