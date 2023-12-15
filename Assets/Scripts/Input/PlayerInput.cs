using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Player Input", fileName = "Player Input")]
public class PlayerInput : ScriptableObject
{
    private InputActions inputActions;

    public Vector2 Axis =>inputActions.GamePlay.Axis.ReadValue<Vector2>();


    public bool Jump => inputActions.GamePlay.Jump.WasPerformedThisFrame();
    public bool StopJump => inputActions.GamePlay.Jump.WasReleasedThisFrame();

    public bool Dash => inputActions.GamePlay.Dash.WasPerformedThisFrame();

    private void OnEnable()
    {
        inputActions = new InputActions();

        inputActions.Enable();

        EventCenter.Subscribe(EventNames.Respawn, DisableAllInputs);

        EventCenter.Subscribe(EventNames.Playing, EnableGameplayInput);
    }

    private void OnDisable()
    {
        DisableAllInputs();

        EventCenter.Unsubscribe(EventNames.Respawn,DisableAllInputs);

        EventCenter.Unsubscribe(EventNames.Playing, EnableGameplayInput);
    }

    /// <summary>
    /// �L��actionmap��ς��
    /// </summary>
    /// <param name="actionMap">�ς�����actionMap</param>
    /// <param name="isUIInput">UI�̑I����</param>
    void SwitchActionMap(InputActionMap actionMap, bool isUIInput)
    {
        inputActions.Disable();
        actionMap.Enable();

        if (isUIInput)
        {
            Cursor.visible = true;                     // �}�E�X�J�[�\�������ɂ��܂��B
            Cursor.lockState = CursorLockMode.None;    // �}�E�X�J�[�\�������b�N���Ȃ��B
        }
        else
        {
            Cursor.visible = false;                     // �}�E�X�J�[�\����s���ɂ��܂��B
            Cursor.lockState = CursorLockMode.Locked;   // �}�E�X�J�[�\�������b�N����B
        }
    }

    /// <summary>
    /// ���͂𖳌�������
    /// </summary>
    private void DisableAllInputs() => inputActions.Disable();

    /// <summary>
    /// �Q�[�����ŃL�����N�^�[�𑀍삷�鎞�ɓ��͂�L�������郁�\�b�h�B
    /// </summary>
    private void EnableGameplayInput() => SwitchActionMap(inputActions.GamePlay, false);
}
