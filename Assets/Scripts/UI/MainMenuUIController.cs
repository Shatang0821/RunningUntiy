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
    }

    /// <summary>
    /// ���j���[��ʂ������āA�Q�[���V�[���ɑJ��
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
    /// �Q�[�����I��
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
