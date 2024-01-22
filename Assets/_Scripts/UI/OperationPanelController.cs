using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OperationPanelController : MonoBehaviour
{
    private void OnEnable()
    {
        UIInput.Instance.DisableUIInputs();
    }

    private void OnDisable()
    {
        UIInput.Instance.EnableUIInputs();
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
