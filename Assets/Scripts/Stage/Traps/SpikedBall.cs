using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedBall : MonoBehaviour
{
    [SerializeField] private Transform centerPoint; // 円運動の中心
    [SerializeField] private GameObject chainPrefab; // チェインのプレハブ
    [SerializeField] private GameObject ballPrefab; // スパイクボールのプレハブ

    [SerializeField] private List<GameObject> chains;

    [SerializeField] private int numberOfChains;
                     //private float baseRadius = 0.5f; // 基本となる半径
    [SerializeField] private float chainSpacing = 0.5f; // チェイン間の距離

    [SerializeField] private float speed = 1f; // 円運動の速度
    [SerializeField] private float startAngle = 0f;
    [SerializeField] private float endAngle = 180f;

    private float currentAngle = 0f;
    private bool movingForward = true; // 移動方向を追跡するフラグ

    private void Start()
    {
        currentAngle = 0;

        SetInitPos();
    }

    void Update()
    {
        CircularMotion();
    }

    private void SetInitPos()
    {
        // 初期角度をラジアンに変換します
        float angle = startAngle * Mathf.Deg2Rad;

        for (int i = 0; i < chains.Count; i++)
        {
            // チェインごとの半径を計算します
            float chainRadius = chainSpacing * i;

            // チェインの位置を計算します
            Vector3 chainPosition = centerPoint.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * chainRadius;

            // チェインの位置を設定します
            chains[i].transform.position = chainPosition;
        }
        // ボールの位置を設定します。ボールはすべてのチェインの末尾に配置されます
        var lastPostion = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * (chainSpacing * chains.Count);
        ballPrefab.transform.position = centerPoint.position + lastPostion;
    }

    private void CircularMotion()
    {
        currentAngle += (movingForward ? speed : -speed) * Time.deltaTime;
        currentAngle = Mathf.Clamp(currentAngle, startAngle, endAngle);

        for (int i = 0; i < chains.Count; i++)
        {
            float chainRadius = chainSpacing * i;

            Vector3 chainPosition = centerPoint.position + new Vector3(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad), 0) * chainRadius;
            
            chains[i].transform.position = chainPosition;
        }

        var lastPostion = new Vector3(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad), 0) * (chainSpacing * chains.Count);
        ballPrefab.transform.position = centerPoint.position + lastPostion;

        if (currentAngle <= startAngle || currentAngle >= endAngle)
        {
            movingForward = !movingForward;
        }
    }



    public void AddChain()
    {
        //チェインを作る
        GameObject newChain = Instantiate(chainPrefab, this.transform);
        //newChain.transform.localPosition = new Vector3(0, -chainSpacing * chains.Count, 0);
        chains.Add(newChain);
    }
}
