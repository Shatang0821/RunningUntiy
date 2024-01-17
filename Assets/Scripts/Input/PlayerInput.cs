using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Player Input", fileName = "Player Input")]
public class PlayerInput : ScriptableObject
{
    private InputActions inputActions;

    public Vector2 Axis =>inputActions.GamePlay.Axis.ReadValue<Vector2>();


    public bool Jump => inputActions.GamePlay.Jump.WasPerformedThisFrame();         //�W�����v�L�[�������ꂽ�Ƃ�
    //public bool StopJump => inputActions.GamePlay.Jump.WasReleasedThisFrame();    //�W�����v�����ꂽ�Ƃ�

    public bool Dash => inputActions.GamePlay.Dash.WasPerformedThisFrame();         //�_�b�V���L�[�������ꂽ�Ƃ�

    public bool Climb => inputActions.GamePlay.Climb.IsPressed();                   //�o��L�[�����������Ă����

    private Gamepad gamepad;

    private void OnEnable()
    {
        inputActions = new InputActions();

        gamepad = Gamepad.current;

        inputActions.Enable();

        //�L�[�C�x���g�̃T�u�X�N���C�u
        inputActions.GamePlay.Pause.performed += context => OnPause(context);
        inputActions.PauseMenu.Unpause.performed += context => UnPause(context);

        //EventCenter.Subscribe(StateEvents.SpawnPlayer, DisableAllInputs);

        //EventCenter.Subscribe(StateEvents.Playing, EnableGameplayInput);

        EventCenter.Subscribe(InputEvents.EnableGameInput, EnableGameplayInput);
        EventCenter.Subscribe(InputEvents.EnablePauseMenuInput, EnablePauseMenuInput);

        EventCenter.Subscribe(InputEvents.DynamicInput, SwitchToDynamicUpdateMode);

        EventCenter.Subscribe(InputEvents.disableAllInput, DisableAllInputs);

        EventCenter.Subscribe(InputEvents.GamepadVibration, VibrateGamepad);
        EventCenter.Subscribe(InputEvents.StopGamepadVibration, StopVibrateGamepad);

    }

    private void OnDisable()
    {
        DisableAllInputs();

        inputActions.GamePlay.Pause.performed -= context => OnPause(context);
        inputActions.PauseMenu.Unpause.performed -= context => UnPause(context);

        EventCenter.Unsubscribe(StateEvents.SpawnPlayer,DisableAllInputs);

        EventCenter.Unsubscribe(StateEvents.Playing, EnableGameplayInput);

        EventCenter.Unsubscribe(InputEvents.EnableGameInput, EnableGameplayInput);
        EventCenter.Unsubscribe(InputEvents.EnablePauseMenuInput, EnablePauseMenuInput);

        EventCenter.Unsubscribe(InputEvents.DynamicInput, SwitchToDynamicUpdateMode);

        EventCenter.Unsubscribe(InputEvents.disableAllInput, DisableAllInputs);

        EventCenter.Unsubscribe(InputEvents.GamepadVibration, VibrateGamepad);
        EventCenter.Unsubscribe(InputEvents.StopGamepadVibration, StopVibrateGamepad);

        // �U�����~���A�p�����[�^���Z�b�g
        InputSystem.ResetHaptics();
    }

    #region InputSettings
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
    /// ���͂�ProcessEventsInDynamicUpdate�ɕς���
    /// </summary>
    //public void SwitchToDynamicUpdateMode() => InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInDynamicUpdate;
    public void SwitchToDynamicUpdateMode()
    {
        Debug.Log(InputSystem.settings.updateMode.ToString());
        InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInDynamicUpdate;
        Debug.Log(InputSystem.settings.updateMode.ToString());
    }
    /// <summary>
    /// ���͂�ProcessEventsInFixedUpdate�ɕς���
    /// </summary>
    //public void SwitchToFixedUpdateMode() => InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInFixedUpdate;
    public void SwitchToFixedUpdateMode()
    {
        Debug.Log(InputSystem.settings.updateMode.ToString());
        InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInFixedUpdate;
        Debug.Log(InputSystem.settings.updateMode.ToString());
    }
    /// <summary>
    /// �Q�[�����ŃL�����N�^�[�𑀍삷�鎞�ɓ��͂�L�������郁�\�b�h�B
    /// </summary>
    private void EnableGameplayInput() => SwitchActionMap(inputActions.GamePlay, false);

    /// <summary>
    /// UI���̓}�b�v�ɐ؂�ւ���
    /// </summary>
    private void EnablePauseMenuInput() => SwitchActionMap(inputActions.PauseMenu,true);

    #endregion
    #region Pause
    private void OnPause(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            OnPause();
        }
    }

    private void UnPause(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            UnPause();
        }
    }

    /// <summary>
    /// �ꎞ��~�����邽�߂ɕK�v�ȏ���
    /// </summary>
    private void OnPause()
    {
        EventCenter.TriggerEvent(InputEvents.EnablePauseMenuInput);   //InputMap��UIInput�ɕς���
        EventCenter.TriggerEvent(TimeEvents.StopTime);          //TimeScale��0�ɂ���
        EventCenter.TriggerEvent(UIEvents.ShowMenuBar);         //���j���[��\��������
    }

    private void UnPause()
    {
        EventCenter.TriggerEvent(InputEvents.EnableGameInput);
        EventCenter.TriggerEvent(TimeEvents.StartTime);
        EventCenter.TriggerEvent(UIEvents.HideMenuBar);
    }
    #endregion

    #region GamePad

    private void VibrateGamepad()
    {
        if (gamepad == null)
            return;
        gamepad.SetMotorSpeeds(1, 1);
    }

    private void StopVibrateGamepad()
    {
        if (gamepad == null)
            return;
        gamepad.SetMotorSpeeds(0, 0);
    }

    #endregion
}
