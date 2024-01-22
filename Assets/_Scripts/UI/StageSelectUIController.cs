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
        UIInput.Instance.DisableUIInputs();
    }

    public void LoadStage1()
    {
        SceneLoader.Instance.LoadStage1Scene();
        UIInput.Instance.DisableUIInputs();
    }

    public void LoadTutorial()
    {
        Debug.LogWarning("Loading");
        SceneLoader.Instance.LoadTutorialScene();
        UIInput.Instance.DisableUIInputs();
    }
}
