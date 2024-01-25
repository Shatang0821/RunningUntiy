using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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

        // �U�����~���A�p�����[�^���Z�b�g
        InputSystem.ResetHaptics();

    }

    private void OnDisable()
    {
        EventCenter.Unsubscribe(ButtonEvents.startButton, OnStartButtonClicked);
        EventCenter.Unsubscribe(ButtonEvents.optionButton, OnOptionButtonClicked);
        EventCenter.Unsubscribe(ButtonEvents.quitButton, OnQuitButtonClicked);

        // �U�����~���A�p�����[�^���Z�b�g
        InputSystem.ResetHaptics();
    }

    private void Start()
    {
        Cursor.visible = false;                     // �}�E�X�J�[�\����s���ɂ��܂��B
        Cursor.lockState = CursorLockMode.Locked;   // �}�E�X�J�[�\�������b�N����B

        GameManager.GameState = GameState.MainMenu;
        UIInput.Instance.SelectUI(buttonStart);
        operationPanel.SetActive(false);
    }

    /// <summary>
    /// ���j���[��ʂ������āA�Q�[���V�[���ɑJ��
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
