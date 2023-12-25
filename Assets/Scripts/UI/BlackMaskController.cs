using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackMaskController : Singleton<BlackMaskController>
{
    Material material;
    [SerializeField] Camera uiCamera;
    Image image;
    protected override void Awake()
    {
        base.Awake();
        image = GetComponent<Image>();
        material = image.material;

        image.enabled = false;
    }


    /// <summary>
    /// カメラ中心でマスクを徐々に拡大
    /// </summary>
    /// <param name="startRadius">初期半径</param>
    /// <param name="delay">初期から拡大開始までの待ち時間</param>
    /// <returns></returns>
    public IEnumerator ScaleInOut(float startRadius,float delay)
    {
        image.enabled = true;

        // カメラが移動しても中心を保持するために、毎フレーム中心座標を更新する
        Vector3 screenCenter = uiCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0f));

        // RectTransform を取得
        RectTransform rectTransform = GetComponent<RectTransform>();

        // 定義半径の開始値と終了値
        float endRadius = 2000f;

        // 拡大にかかる時間
        float duration = 0.5f;

        // カウンタ
        float elapsedTime = 0f;

        // 初期値を設定する
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, screenCenter, uiCamera, out Vector2 canvasCenter);
        material.SetVector("_Center", new Vector4(canvasCenter.x, canvasCenter.y, 0, 0));
        material.SetFloat("_Radius", startRadius);
        yield return new WaitForSeconds(delay);

        while (elapsedTime < duration)
        {
            

            // 現在の時間での半径の値を計算する
            float currentRadius = Mathf.Lerp(startRadius, endRadius, elapsedTime / duration);

            // シェーダーの_Radiusパラメータを更新する
            material.SetFloat("_Radius", currentRadius);

            // 次のフレームまで待機し、経過時間を更新する
            yield return null;
            elapsedTime += Time.deltaTime;
        }

        // 最終的な半径の値を設定する
        material.SetFloat("_Radius", endRadius);

        // 半径の拡大が完了したら、GameObjectを非表示にする
        image.enabled = false;
    }

    /// <summary>
    /// 指定座標を中心でマスクを徐々に拡大
    /// </summary>
    /// <param name="position">指定中心座標</param>
    /// <param name="startRadius">初期半径</param>
    /// <param name="delay">初期から拡大開始までの待ち時間</param>
    /// <returns></returns>
    public IEnumerator ScaleInOut(GameObject gameObject, float startRadius, float delay)
    {
        image.enabled = true;

        // RectTransform を取得
        RectTransform rectTransform = GetComponent<RectTransform>();

        // 定義半径の終了値
        float endRadius = 2000f;

        // 拡大にかかる時間
        float duration = 0.5f;

        // カウンタ
        float elapsedTime = 0f;

        // 初期値を設定する
        material.SetFloat("_Radius", startRadius);
        while (elapsedTime < delay)
        {
            // カメラのビューポートを考慮してプレイヤーの位置をスクリーン座標に変換する
            Vector3 screenCenter = uiCamera.WorldToScreenPoint(gameObject.transform.position);
            // スクリーン座標からキャンバス内のローカル座標に変換する
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, screenCenter, uiCamera, out Vector2 canvasCenter);
            material.SetVector("_Center", new Vector4(canvasCenter.x, canvasCenter.y, 0, 0));

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //yield return new WaitForSeconds(delay);

        elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            

            // 現在の時間での半径の値を計算する
            float currentRadius = Mathf.Lerp(startRadius, endRadius, elapsedTime / duration);

            // シェーダーの_Radiusパラメータを更新する
            material.SetFloat("_Radius", currentRadius);

            // 次のフレームまで待機し、経過時間を更新する
            yield return null;
            elapsedTime += Time.deltaTime;
        }

        // 最終的な半径の値を設定する
        material.SetFloat("_Radius", endRadius);

        // 半径の拡大が完了したら、GameObjectを非表示にする
        image.enabled = false;
    }

}
