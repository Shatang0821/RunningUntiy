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


    // ScaleInOutコルーチンは、マスクの半径を徐々に拡大していく
    public IEnumerator ScaleInOut(Vector3 worldCenter, float delay)
    {
        yield return new WaitForSeconds(delay);
        image.enabled = true;

        // 将世界坐标转换为屏幕坐标
        Vector3 screenCenter = uiCamera.WorldToScreenPoint(worldCenter);

        // 将屏幕坐标转换为Canvas坐标
        Vector2 canvasCenter;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(), screenCenter, uiCamera, out canvasCenter);

        // 使用转换后的Canvas坐标作为Shader的_Center参数
        material.SetVector("_Center", new Vector2(canvasCenter.x, canvasCenter.y));

        // 定义半径的开始值和结束值
        float startRadius = 0f;
        float endRadius = 2000f;

        // 定义扩大半径所需的总时间（例如2秒）
        float duration = 0.5f;

        // 用于跟踪过渡进度的计时器
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // 计算当前时间下的半径值
            float currentRadius = Mathf.Lerp(startRadius, endRadius, elapsedTime / duration);

            // 更新Shader的_Radius参数
            material.SetFloat("_Radius", currentRadius);

            // 等待下一帧，并更新经过的时间
            yield return null;
            elapsedTime += Time.deltaTime;
        }

        // 确保最后半径设置为最终值
        material.SetFloat("_Radius", endRadius);

        // 半径の拡大が完了したら、GameObjectを非表示にします。
        image.enabled = false;


    }
}
