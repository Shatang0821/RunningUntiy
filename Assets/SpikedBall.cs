using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedBall : MonoBehaviour
{
    public Transform centerPoint;
    public float radius = 5f;
    public float speed = 1f;
    public float startAngle = 0f;
    public float endAngle = 180f;
    public float lerpSpeed = 0.1f; // 補間の速度
    private float currentAngle = 0f;
    private bool movingForward = true; // 移動方向を追跡するフラグ

    private void Start()
    {
        currentAngle = 0;
    }

    void Update()
    {
        // 現在の移動方向に基づいて角度を更新
        if (movingForward)
        {
            currentAngle += speed * Time.deltaTime;
        }
        else
        {
            currentAngle -= speed * Time.deltaTime;
        }

        // 角度を始点と終点の間に制限
        currentAngle = Mathf.Clamp(currentAngle, startAngle, endAngle);

        // 新しい位置を計算
        Vector2 newPosition = centerPoint.position - new Vector3(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad), 0) * radius;

        // オブジェクトの位置を更新
        transform.position = newPosition;

        // 始点または終点に達したら方向を切り替え
        if (currentAngle <= startAngle || currentAngle >= endAngle)
        {
            movingForward = !movingForward;
        }
    }

    void OnDrawGizmos()
    {
        if (centerPoint == null)
            return;

        Gizmos.color = Color.red; // ギズモの色を設定

        Vector3 start = centerPoint.position + new Vector3(Mathf.Cos(startAngle * Mathf.Deg2Rad), Mathf.Sin(startAngle * Mathf.Deg2Rad), 0) * radius;
        Vector3 end = centerPoint.position + new Vector3(Mathf.Cos(endAngle * Mathf.Deg2Rad), Mathf.Sin(endAngle * Mathf.Deg2Rad), 0) * radius;

        // 始点から終点までの軌道を描画
        float step = 0.1f;
        for (float angle = startAngle; angle <= endAngle; angle += step)
        {
            Vector3 previous = centerPoint.position - new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0) * radius;
            Vector3 next = centerPoint.position - new Vector3(Mathf.Cos((angle + step) * Mathf.Deg2Rad), Mathf.Sin((angle + step) * Mathf.Deg2Rad), 0) * radius;

            Gizmos.DrawLine(previous, next);
        }

        // 始点と終点を強調
        Gizmos.DrawSphere(start, 0.2f);
        Gizmos.DrawSphere(end, 0.2f);
    }
}
