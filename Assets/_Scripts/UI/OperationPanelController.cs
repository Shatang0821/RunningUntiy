using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OperationPanelController : MonoBehaviour
{
    private void OnEnable()
    {
        UIInput.Instance.DisableUIInputs();

        // �U�����~���A�p�����[�^���Z�b�g
        InputSystem.ResetHaptics();
    }

    private void OnDisable()
    {
        UIInput.Instance.EnableUIInputs();

        // �U�����~���A�p�����[�^���Z�b�g
        InputSystem.ResetHaptics();
    }
    // Update is called once per frame
    void Update()
    {
        // �Q�[���p�b�h��B�{�^�����`�F�b�N
        bool isBButtonPressed = Gamepad.current != null && Gamepad.current.buttonEast.isPressed;
        if (isBButtonPressed)
        {
            gameObject.SetActive(false);
            UIInput.Instance.EnableUIInputs();
        }
    }
}
