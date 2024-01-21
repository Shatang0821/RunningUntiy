using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpInputBufferTime = 0.5f;
    [HideInInspector]
    public WaitForSeconds waitJumpInputBufferTime;
    [HideInInspector]
    public bool HasJumpInputBuffer { get; private set; }
    public float customJumpForce { get; private set; }
    public void Initialize()
    {
        waitJumpInputBufferTime = new WaitForSeconds(jumpInputBufferTime);
    }
    private void OnEnable()
    {
        EventCenter.Subscribe(StateEvents.SetCustomJumpForce, SetCustomJumpForce);
    }
    private void OnDisable()
    {
        HasJumpInputBuffer = false;
        EventCenter.Unsubscribe(StateEvents.SetCustomJumpForce, SetCustomJumpForce);
    }

    // ジャンプ入力バッファの設定
    public void SetJumpInputBufferTimer()
    {
        StopCoroutine(nameof(JumpInputBufferCoroutine));
        StartCoroutine(nameof(JumpInputBufferCoroutine));
    }

    IEnumerator JumpInputBufferCoroutine()
    {
        HasJumpInputBuffer = true;
        yield return waitJumpInputBufferTime;
        HasJumpInputBuffer = false;
    }


    public void SetCustomJumpForce(object jumpForceObj)
    {
        if (jumpForceObj is float)
        {
            customJumpForce = (float)jumpForceObj;
        }
    }
}
