using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    #region JUMP
    public float jumpInputBufferTime = 0.5f;
    [HideInInspector]
    public WaitForSeconds waitJumpInputBufferTime;
    [HideInInspector]
    public bool HasJumpInputBuffer { get; private set; }
    public float customJumpForce { get; private set; }
    #endregion

    #region DASH
    public GameObject dashGhost;
    public bool dashTrigger { get; private set; } // ダッシュ制限
    public Sprite sprite => gameObject.GetComponentInChildren<SpriteRenderer>().sprite;
    public GameObject dashMask;
    private SpriteRenderer dashMaskSprite;
    #endregion
    public void Initialize()
    {
        waitJumpInputBufferTime = new WaitForSeconds(jumpInputBufferTime);
        dashMaskSprite = dashMask.GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        EventCenter.Subscribe(StateEvents.SetCustomJumpForce, SetCustomJumpForce);
        EventCenter.Subscribe(StateEvents.SetDashTrigger, SetDashTrigger);
    }
    private void OnDisable()
    {
        HasJumpInputBuffer = false;
        EventCenter.Unsubscribe(StateEvents.SetCustomJumpForce, SetCustomJumpForce);
        EventCenter.Unsubscribe(StateEvents.SetDashTrigger, SetDashTrigger);
    }

    #region JUMP METHOD
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
    #endregion

    #region DASH METHOD
    private void SetDashTrigger(object canDash)
    {
        if(canDash is bool) 
        {
            dashTrigger = (bool)canDash;
            SetDashMaskAlpha((bool)canDash);
        }
    }

    private void SetDashMaskAlpha(bool canDash)
    {
        Color color = dashMaskSprite.color;
        if (canDash)
        {
            color.a = 0f;
            dashMaskSprite.color = color;
        }
        else
        {
            color.a = 1f;
            dashMaskSprite.color = color;
        }
        
    }
    #endregion
}
