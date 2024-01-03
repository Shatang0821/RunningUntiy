using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPointsMover : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private float moveSpeed;
    private int pointIndex = 0;
    
    private void Start()
    {
        transform.position = points[0].position;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, points[pointIndex].position, moveSpeed * Time.deltaTime);

        if(Vector2.Distance(transform.position, points[pointIndex].position) < 0.1f)
        {
            //transform.position = points[pointIndex].position;
            // インデックスを循環させる
            pointIndex = (pointIndex + 1) % points.Length;
        }
    }
}
