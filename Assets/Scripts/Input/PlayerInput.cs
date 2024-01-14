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

    public bool Climb => inputActions.GamePlay.Climb.IsPressed();

    private void OnEnable()
    {
        inputActions = new InputActions();

        inputActions.Enable();

        inputActions.GamePlay.Pause.performed += context => OnPause(context);
        inputActions.PauseMenu.Unpause.performed += context => UnPause(context);

        EventCenter.Subscribe(EventNames.SpawnPlayer, DisableAllInputs);

        EventCenter.Subscribe(EventNames.Playing, EnableGameplayInput);

        EventCenter.Subscribe(InputNames.onPause, EnablePauseMenuInput);

        EventCenter.Subscribe(InputNames.DynamicInput, SwitchToDynamicUpdateMode);

        EventCenter.Subscribe(ButtonNames.resumeButton, EnableGameplayInput);

        EventCenter.Subscribe(ButtonNames.resumeButton, SwitchToFixedUpdateMode);

        EventCenter.Subscribe(InputNames.disableAllInput, DisableAllInputs);


    }

    private void OnDisable()
    {
        DisableAllInputs();

        inputActions.GamePlay.Pause.performed -= context => OnPause(context);

        EventCenter.Unsubscribe(EventNames.SpawnPlayer,DisableAllInputs);

        EventCenter.Unsubscribe(EventNames.Playing, EnableGameplayInput);

        EventCenter.Unsubscribe(InputNames.onPause, EnablePauseMenuInput);

        EventCenter.Unsubscribe(InputNames.DynamicInput, SwitchToDynamicUpdateMode);

        EventCenter.Unsubscribe(ButtonNames.resumeButton, EnableGameplayInput);

        EventCenter.Unsubscribe(ButtonNames.resumeButton, SwitchToFixedUpdateMode);

        EventCenter.Unsubscribe(InputNames.disableAllInput, DisableAllInputs);
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

    private void EnablePauseMenuInput() => SwitchActionMap(inputActions.PauseMenu,true);

    private void OnPause(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            EventCenter.TriggerEvent(InputNames.onPause);

        }
    }

    private void UnPause(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            EventCenter.TriggerEvent(InputNames.unPause);
            Debug.Log("unPause");
        }
    }
}
