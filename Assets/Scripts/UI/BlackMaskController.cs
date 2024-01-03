using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackMaskController : Singleton<BlackMaskController>
{
    private Material material;
    [SerializeField] private Camera uiCamera;
    private Image image;

    private const float EndRadius = 2000f; // 定義半径の終了値
    private const float Duration = 0.5f; // 拡大にかかる時間

    protected override void Awake()
    {
        base.Awake();
        image = GetComponent<Image>();
        material = image.material;

        image.enabled = false;
    }

    public IEnumerator ScaleInOut(float startRadius, float delay, GameObject gameObject = null)
    {
        image.enabled = true;

        // RectTransform を取得
        RectTransform rectTransform = GetComponent<RectTransform>();

        // カウンタ
        float elapsedTime = 0f;

        // 初期値を設定する
        material.SetFloat("_Radius", startRadius);
        while (elapsedTime < delay)
        {
            Vector3 screenCenter;
            if (gameObject != null)
            {
                // gameObjectがnullでない場合、そのオブジェクトのスクリーン座標を使用
                screenCenter = uiCamera.WorldToScreenPoint(gameObject.transform.position);
            }
            else
            {
                // nullの場合、カメラのビューポートの中心を使用
                screenCenter = uiCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0f));
            }

            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, screenCenter, uiCamera, out Vector2 canvasCenter);
            material.SetVector("_Center", new Vector4(canvasCenter.x, canvasCenter.y, 0, 0));

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0f;
        while (elapsedTime < Duration)
        {
            float currentRadius = Mathf.Lerp(startRadius, EndRadius, elapsedTime / Duration);
            material.SetFloat("_Radius", currentRadius);
            yield return null;
            elapsedTime += Time.deltaTime;
        }

        material.SetFloat("_Radius", EndRadius);
        image.enabled = false;
    }

}
