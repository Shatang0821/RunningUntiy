using UnityEngine;
using UnityEngine.UI;

public class GamePlayUIController : MonoBehaviour
{
    [Header("==== CANVAS ====")]
    [SerializeField] Canvas menusCanvas;

    [Header("==== PLYAER INPUT ====")]
    [SerializeField] Button resumeButton;           //ゲームに戻るボタン

    [SerializeField] Button optionButton;          //オプションボタン

    [SerializeField] Button mainMenuButton;         //メインメニューの戻るボタン

    //int buttonPressedParameterID = Animator.StringToHash("Pressed");//Pressedをハッシュ値の変更する

    GameState currentState;
    private void OnEnable()
    {
        EventCenter.Subscribe(UIEvents.ShowMenuBar, Pause);
        EventCenter.Subscribe(UIEvents.HideMenuBar, UnPause);

        EventCenter.Subscribe(ButtonEvents.resumeButton, OnResumeButtonClicked);
        EventCenter.Subscribe(ButtonEvents.optionButton, OnOptionButtonClicked);
        EventCenter.Subscribe(ButtonEvents.mainMenuButton, OnMainMenuButtonClicked);
    }

    private void OnDisable()
    {
        EventCenter.Unsubscribe(UIEvents.ShowMenuBar, Pause);
        EventCenter.Unsubscribe(UIEvents.HideMenuBar, UnPause);

        EventCenter.Unsubscribe(ButtonEvents.resumeButton, OnResumeButtonClicked);
        EventCenter.Unsubscribe(ButtonEvents.optionButton, OnOptionButtonClicked);
        EventCenter.Unsubscribe(ButtonEvents.mainMenuButton, OnMainMenuButtonClicked);
    }

    /// <summary>
    /// ポーズメニューの表示
    /// </summary>
    private void Pause()
    {
        menusCanvas.enabled = true;

        UIInput.Instance.SelectUI(resumeButton);
    }

    private void UnPause()
    {
        menusCanvas.enabled = false;
    }

    private void OnResumeButtonClicked()
    {
        EventCenter.TriggerEvent(InputEvents.EnableGameInput);
        EventCenter.TriggerEvent(TimeEvents.StartTime);
        EventCenter.TriggerEvent(UIEvents.HideMenuBar);
    }

    private void OnOptionButtonClicked()
    {

    }

    private void OnMainMenuButtonClicked()
    {
        EventCenter.TriggerEvent(TimeEvents.StartTime);
        EventCenter.TriggerEvent(InputEvents.FixedInput);
        SceneLoader.Instance.LoadMainMenuScene();
    }
}
