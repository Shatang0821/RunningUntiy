using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ギミックのパトロール制御クラス
/// </summary>
public class PatrolPointsMover : MonoBehaviour
{
    private enum PatrolType
    {
        Loop,     // 1234, 1234...
        Static,   //静的
        PingPong  // 1234321, 1234321...
    }
    [Header("Move Info")]
    [SerializeField] private PatrolType patrolType; // 列挙型を使う

    [SerializeField] public List<Transform> points; //移動先
    [SerializeField] private float moveSpeed;       //移動速度
    
    private int pointIndex = 0;//移動先添え字
    private bool isGoingForward = true; //ポイントに向かって前進しているかを追跡
    private void Awake()
    {
        if(points.Count == 0)
        {
            patrolType = PatrolType.Static;
        }
    }
    private void OnEnable()
    {
        if (points.Count > 0)
        {
            transform.position = points[0].position;
            StartCoroutine(nameof(MoveToPoints));
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
    private IEnumerator MoveToPoints()
    {
        //生きているならずっと動く
        while (this.isActiveAndEnabled)
        {
            Transform currentPoint = points[pointIndex];
            while (Vector2.Distance(transform.position, currentPoint.position) > 0.1f)
            {
                transform.position = Vector2.MoveTowards(transform.position, currentPoint.position, moveSpeed * Time.deltaTime);
                yield return null;
            }

            UpdatePointIndex();
            yield return null;
        }
    }

    /// <summary>
    /// 動きパターン制御
    /// </summary>
    private void UpdatePointIndex()
    {
        switch (patrolType)
        {
            case PatrolType.Loop:
                pointIndex = (pointIndex + 1) % points.Count;
                break;

            case PatrolType.PingPong:
                // pointIndex が増加または減少するかを決定
                if ((pointIndex >= points.Count - 1 && isGoingForward) || (pointIndex <= 0 && !isGoingForward))
                {
                    isGoingForward = !isGoingForward; // 方向を反転
                }

                // pointIndex を更新
                pointIndex += isGoingForward ? 1 : -1;
                break;
        }
    }

}
