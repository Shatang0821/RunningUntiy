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
    /// 有効actionmapを変わり
    /// </summary>
    /// <param name="actionMap">変えたいactionMap</param>
    /// <param name="isUIInput">UIの選択か</param>
    void SwitchActionMap(InputActionMap actionMap, bool isUIInput)
    {
        inputActions.Disable();
        actionMap.Enable();

        if (isUIInput)
        {
            Cursor.visible = true;                     // マウスカーソルを可視にします。
            Cursor.lockState = CursorLockMode.None;    // マウスカーソルをロックしない。
        }
        else
        {
            Cursor.visible = false;                     // マウスカーソルを不可視にします。
            Cursor.lockState = CursorLockMode.Locked;   // マウスカーソルをロックする。
        }
    }

    /// <summary>
    /// 入力を無効化する
    /// </summary>
    private void DisableAllInputs() => inputActions.Disable();

    /// <summary>
    /// 入力をProcessEventsInDynamicUpdateに変える
    /// </summary>
    //public void SwitchToDynamicUpdateMode() => InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInDynamicUpdate;
    public void SwitchToDynamicUpdateMode()
    {
        Debug.Log(InputSystem.settings.updateMode.ToString());
        InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInDynamicUpdate;
        Debug.Log(InputSystem.settings.updateMode.ToString());
    }
    /// <summary>
    /// 入力をProcessEventsInFixedUpdateに変える
    /// </summary>
    //public void SwitchToFixedUpdateMode() => InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInFixedUpdate;
    public void SwitchToFixedUpdateMode()
    {
        Debug.Log(InputSystem.settings.updateMode.ToString());
        InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInFixedUpdate;
        Debug.Log(InputSystem.settings.updateMode.ToString());
    }
    /// <summary>
    /// ゲーム内でキャラクターを操作する時に入力を有効化するメソッド。
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
