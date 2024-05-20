using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Player Input", fileName = "Player Input")]
public class PlayerInput : ScriptableObject
{
    private InputActions inputActions;

    public Vector2 Axis => 
        DirectionInput(inputActions.GamePlay.Axis.ReadValue<Vector2>().x, inputActions.GamePlay.Axis.ReadValue<Vector2>().y);

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
    #region INPUT


    /// <summary>
    /// 入力によってダッシュ方向を設定する
    /// コントローラのためである
    /// </summary>
    /// <returns>ダッシュ方向</returns>
    Vector2 DirectionInput(float x, float y)
    {
        // 入力がゼロかどうかをチェック
        if (x == 0 && y == 0)
        {
            return Vector2.zero;
        }

        // 入力から角度を計算
        float angleInRadians = Mathf.Atan2(y, x);
        float angleInDegrees = angleInRadians * Mathf.Rad2Deg;

        // 角度が負の場合、360度の範囲に変換
        if (angleInDegrees < 0)
        {
            angleInDegrees += 360;
        }

        // 角度で方向を判断する
        switch ((int)angleInDegrees)
        {
            case int n when (n >= 337.5 || n < 22.5):
                return Vector2.right; // 右
            case int n when (n >= 22.5 && n < 67.5):
                return new Vector2(1, 1); // 右上
            case int n when (n >= 67.5 && n < 112.5):
                return Vector2.up; // 上
            case int n when (n >= 112.5 && n < 157.5):
                return new Vector2(-1, 1); // 左上
            case int n when (n >= 157.5 && n < 202.5):
                return Vector2.left; // 左
            case int n when (n >= 202.5 && n < 247.5):
                return new Vector2(-1, -1); // 左下
            case int n when (n >= 247.5 && n < 292.5):
                return Vector2.down; // 下
            default:
                return new Vector2(1, -1); // 右下
        }
    }

    #endregion

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
        InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInDynamicUpdate;
    }
    /// <summary>
    /// 入力をProcessEventsInFixedUpdateに変える
    /// </summary>
    //public void SwitchToFixedUpdateMode() => InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInFixedUpdate;
    public void SwitchToFixedUpdateMode()
    {
        InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInFixedUpdate;
    }
    /// <summary>
    /// ゲーム内でキャラクターを操作する時に入力を有効化するメソッド。
    /// </summary>
    private void EnableGameplayInput() => SwitchActionMap(inputActions.GamePlay, false);

    /// <summary>
    /// UI入力マップに切り替える
    /// </summary>
    private void EnablePauseMenuInput() => SwitchActionMap(inputActions.PauseMenu,false);

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
        EventCenter.TriggerEvent(UIEvents.UnPause);
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
