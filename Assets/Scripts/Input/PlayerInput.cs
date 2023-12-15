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
    /// ゲーム内でキャラクターを操作する時に入力を有効化するメソッド。
    /// </summary>
    private void EnableGameplayInput() => SwitchActionMap(inputActions.GamePlay, false);
}
