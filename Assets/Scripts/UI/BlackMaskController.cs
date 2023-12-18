using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackMaskController : Singleton<BlackMaskController>
{
    Material material;
    [SerializeField] GameObject player;
    [SerializeField] Camera uiCamera;
    Image image;
    protected override void Awake()
    {
        base.Awake();
        image = GetComponent<Image>();
        material = image.material;

        image.enabled = false;
    }



    private void OnEnable()
    {
        EventCenter.Subscribe(EventNames.Respawn, ShowMask);

    }

    private void OnDisable()
    {
        EventCenter.Unsubscribe(EventNames.Respawn, ShowMask);
    }

    void ShowMask()
    {
        StartCoroutine(ScaleInOut());
    }

    // ScaleInOutコルーチンは、マスクの半径を徐々に拡大していく
    IEnumerator ScaleInOut()
    {
        yield return new WaitForSeconds(0.5f);
        image.enabled = true;
        // プレイヤーの世界座標をスクリーン座標に変換します。
        Vector3 playerScreenPos = uiCamera.WorldToScreenPoint(player.transform.position);

        // スクリーン座標をCanvasのローカル座標に変換します。
        Vector2 canvasPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(), playerScreenPos, uiCamera, out canvasPos);

        // Shaderの_Centerパラメータを設定します。
        material.SetVector("_Center", new Vector2(canvasPos.x, canvasPos.y));

        // 半径が0から始まり、徐々に増加していきます。
        for (float r = 0; r < 2000; r += 50)
        {
            // Shaderの_Radiusパラメータを更新します。
            material.SetFloat("_Radius", r);
            // 次のフレームまで待機します。
            yield return null;
        }

        // 半径の拡大が完了したら、GameObjectを非表示にします。
        image.enabled = false;

        GameManager.GameState = GameState.Playing;

        EventCenter.TriggerEvent(EventNames.Playing);
    }
}
