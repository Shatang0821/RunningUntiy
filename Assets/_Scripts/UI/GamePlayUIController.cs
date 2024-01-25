using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GamePlayUIController : MonoBehaviour
{
    [Header("==== CANVAS ====")]
    [SerializeField] Canvas menusCanvas;

    [Header("==== PANEL ====")]
    [SerializeField] GameObject operationPanel;

    [Header("==== PLYAER INPUT ====")]
    [SerializeField] Button resumeButton;           //�Q�[���ɖ߂�{�^��

    [SerializeField] Button optionButton;          //�I�v�V�����{�^��

    [SerializeField] Button mainMenuButton;         //���C�����j���[�̖߂�{�^��

    int buttonPressedParameterID = Animator.StringToHash("Pressed");//Pressed���n�b�V���l�̕ύX����
    GameState currentState;

    private void Start()
    {
        operationPanel.SetActive(false);
        menusCanvas.enabled = false;
    }
    private void OnEnable()
    {
        EventCenter.Subscribe(UIEvents.ShowMenuBar, Pause);

        EventCenter.Subscribe(UIEvents.UnPause, UnPause);
        EventCenter.Subscribe(ButtonEvents.resumeButton, OnResumeButtonClicked);
        EventCenter.Subscribe(ButtonEvents.optionButton, OnOptionButtonClicked);
        EventCenter.Subscribe(ButtonEvents.mainMenuButton, OnMainMenuButtonClicked);

        // �U�����~���A�p�����[�^���Z�b�g
        InputSystem.ResetHaptics();
    }

    private void OnDisable()
    {
        EventCenter.Unsubscribe(UIEvents.ShowMenuBar, Pause);

        EventCenter.Unsubscribe(UIEvents.UnPause, UnPause);
        EventCenter.Unsubscribe(ButtonEvents.resumeButton, OnResumeButtonClicked);
        EventCenter.Unsubscribe(ButtonEvents.optionButton, OnOptionButtonClicked);
        EventCenter.Unsubscribe(ButtonEvents.mainMenuButton, OnMainMenuButtonClicked);

        // �U�����~���A�p�����[�^���Z�b�g
        InputSystem.ResetHaptics();
    }

    /// <summary>
    /// �|�[�Y���j���[�̕\��
    /// </summary>
    private void Pause()
    {
        menusCanvas.enabled = true;

        UIInput.Instance.SelectUI(resumeButton);
    }

    private void UnPause()
    {
        UIInput.Instance.SelectUI(resumeButton);
        resumeButton.animator.SetTrigger(buttonPressedParameterID);
    }

    private void OnResumeButtonClicked()
    {
        EventCenter.TriggerEvent(InputEvents.EnableGameInput);
        EventCenter.TriggerEvent(TimeEvents.StartTime);
        menusCanvas.enabled = false;
        UIInput.Instance.DeselectUI();
    }

    private void OnOptionButtonClicked()
    {
        operationPanel.SetActive(true);
        UIInput.Instance.DisableUIInputs();
    }

    private void OnMainMenuButtonClicked()
    {
        EventCenter.TriggerEvent(TimeEvents.StartTime);
        EventCenter.TriggerEvent(InputEvents.FixedInput);
        SceneLoader.Instance.LoadMainMenuScene();
    }
}
