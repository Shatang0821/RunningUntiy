using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class StageSelectUIController : MonoBehaviour
{
    [Header("==== CANVAS ====")]
    [SerializeField] Canvas menusCanvas;

    [Header("==== Button ====")]
    [SerializeField] Button[] stages;         //メインメニューの戻るボタン

    private bool isReturn = false;
    // Start is called before the first frame update

    private void Start()
    {
        GameManager.GameState = GameState.MainMenu;
        UIInput.Instance.SelectUI(stages[0]);
    }

    private void OnEnable()
    {
        EventCenter.Subscribe(stages[0].name, LoadTutorial);
        EventCenter.Subscribe(stages[1].name, LoadStage1);

        // 振動を停止し、パラメータリセット
        InputSystem.ResetHaptics();
    }

    private void OnDisable()
    {
        EventCenter.Unsubscribe(stages[0].name, LoadTutorial);
        EventCenter.Unsubscribe(stages[1].name, LoadStage1);

        // 振動を停止し、パラメータリセット
        InputSystem.ResetHaptics();
    }

    private void Update()
    {
        // ゲームパッドのBボタンをチェック
        bool isBButtonPressed = Gamepad.current != null && Gamepad.current.buttonEast.isPressed;

        // キーボードのESCボタンをチェック
        bool isEscPressed = Keyboard.current != null && Keyboard.current.escapeKey.isPressed;

        if ((isBButtonPressed || isEscPressed) && isReturn == false)
        {
            LoadMainMenu();
            isReturn = true;
        }
    }

    private void LoadMainMenu()
    {
        SceneLoader.Instance.LoadMainMenuScene();
    }

    public void LoadStage1()
    {
        SceneLoader.Instance.LoadStage1Scene();
    }

    public void LoadTutorial()
    {
        SceneLoader.Instance.LoadTutorialScene();
    }
}
