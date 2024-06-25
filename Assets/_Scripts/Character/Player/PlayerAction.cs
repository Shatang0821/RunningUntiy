using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    #region JUMP
    public float jumpInputBufferTime = 0.5f;            //ジャンプバッファの継続時間
    [HideInInspector]
    public WaitForSeconds waitJumpInputBufferTime;
    [HideInInspector]
    public bool HasJumpInputBuffer { get; private set; }
    public float customJumpForce { get; private set; }  //カスタムジャンプの力設定      
    #endregion

    #region DASH
    public GameObject dashGhost;                        //ダッシュ残像
    public bool dashTrigger { get; private set; }       // ダッシュ制限
    public Sprite sprite => gameObject.GetComponentInChildren<SpriteRenderer>().sprite;//現在スプライトの取得
    public GameObject dashMask;                                                        //ダッシュ可能かを示すオブジェクト
    private SpriteRenderer dashMaskSprite;                                             //オブジェクトのスプライト
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
    /// <summary>
    /// ジャンプ入力バッファの設定
    /// </summary>
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
    
    /// <summary>
    /// ジャンプ力の設定
    /// </summary>
    /// <param name="jumpForceObj">ジャンプ力</param>
    public void SetCustomJumpForce(object jumpForceObj)
    {
        if (jumpForceObj is float)
        {
            customJumpForce = (float)jumpForceObj;
        }
    }
    #endregion

    #region DASH METHOD
    /// <summary>
    /// ダッシュのトリガー設定
    /// </summary>
    /// <param name="canDash">トリガー(Bool)</param>
    private void SetDashTrigger(object canDash)
    {
        if(canDash is bool) 
        {
            dashTrigger = (bool)canDash;
            SetDashMaskAlpha((bool)canDash);
        }
    }

    /// <summary>
    /// ダッシュ可能かを示すマスクの色設定
    /// </summary>
    /// <param name="canDash">ダッシュ可能か</param>
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
