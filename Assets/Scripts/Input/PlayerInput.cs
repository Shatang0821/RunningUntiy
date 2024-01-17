using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Player Input", fileName = "Player Input")]
public class PlayerInput : ScriptableObject
{
    private InputActions inputActions;

    public Vector2 Axis =>inputActions.GamePlay.Axis.ReadValue<Vector2>();


    public bool Jump => inputActions.GamePlay.Jump.WasPerformedThisFrame();         //ジャンプキーが押されたとき
    //public bool StopJump => inputActions.GamePlay.Jump.WasReleasedThisFrame();    //ジャンプが離れたとき

    public bool Dash => inputActions.GamePlay.Dash.WasPerformedThisFrame();         //ダッシュキーが押されたとき

    public bool Climb => inputActions.GamePlay.Climb.IsPressed();                   //登るキーが押し続けている間

    private Gamepad gamepad;

    private void OnEnable()
    {
        inputActions = new InputActions();

        gamepad = Gamepad.current;

        inputActions.Enable();

        //キーイベントのサブスクライブ
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

        // 振動を停止し、パラメータリセット
        InputSystem.ResetHaptics();
    }

    #region InputSettings
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

    /// <summary>
    /// UI入力マップに切り替える
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
    /// 一時停止中するために必要な処理
    /// </summary>
    private void OnPause()
    {
        EventCenter.TriggerEvent(InputEvents.EnablePauseMenuInput);   //InputMapをUIInputに変える
        EventCenter.TriggerEvent(TimeEvents.StopTime);          //TimeScaleを0にする
        EventCenter.TriggerEvent(UIEvents.ShowMenuBar);         //メニューを表示させる
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
