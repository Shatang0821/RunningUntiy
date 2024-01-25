using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OperationPanelController : MonoBehaviour
{
    private void OnEnable()
    {
        UIInput.Instance.DisableUIInputs();

        // 振動を停止し、パラメータリセット
        InputSystem.ResetHaptics();
    }

    private void OnDisable()
    {
        UIInput.Instance.EnableUIInputs();

        // 振動を停止し、パラメータリセット
        InputSystem.ResetHaptics();
    }
    // Update is called once per frame
    void Update()
    {
        // ゲームパッドのBボタンをチェック
        bool isBButtonPressed = Gamepad.current != null && Gamepad.current.buttonEast.isPressed;
        if (isBButtonPressed)
        {
            gameObject.SetActive(false);
            UIInput.Instance.EnableUIInputs();
        }
    }
}
