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

    int buttonPressedParameterID = Animator.StringToHash("Pressed");//Pressedをハッシュ値の変更する

    GameState currentState;
    private void OnEnable()
    {
        EventCenter.Subscribe(InputNames.onPause, Pause);
        EventCenter.Subscribe(InputNames.unPause, UnPause);

        EventCenter.Subscribe(ButtonNames.resumeButton, OnResumeButtonClicked);
        EventCenter.Subscribe(ButtonNames.optionButton, OnOptionButtonClicked);
        EventCenter.Subscribe(ButtonNames.mainMenuButton, OnMainMenuButtonClicked);
    }

    private void OnDisable()
    {
        EventCenter.Unsubscribe(InputNames.onPause, Pause);
        EventCenter.Unsubscribe(InputNames.unPause, UnPause);

        EventCenter.Unsubscribe(ButtonNames.resumeButton, OnResumeButtonClicked);
        EventCenter.Unsubscribe(ButtonNames.optionButton, OnOptionButtonClicked);
        EventCenter.Unsubscribe(ButtonNames.mainMenuButton, OnMainMenuButtonClicked);
    }

    private void Pause()
    {
        menusCanvas.enabled = true;
        currentState = GameManager.GameState;
        GameManager.GameState = GameState.Paused;

        UIInput.Instance.SelectUI(resumeButton);
    }

    private void UnPause()
    {
        resumeButton.Select();
        resumeButton.animator.SetTrigger(buttonPressedParameterID);//resumeButtonの押されたアニメーションをスタートさせる
    }

    private void OnResumeButtonClicked()
    {
        EventCenter.TriggerEvent(InputNames.unPause);

        menusCanvas.enabled = false;
        GameManager.GameState = currentState;
    }

    private void OnOptionButtonClicked()
    {

    }

    private void OnMainMenuButtonClicked()
    {

    }
}
