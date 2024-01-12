using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PatrolPointsMover : MonoBehaviour
{
    [SerializeField] public List<Transform> points;
    [SerializeField] private float moveSpeed;
    private int pointIndex = 0;
    
    private void Start()
    {
        if(points.Count > 0)
        {
            transform.position = points[0].position;
            StartCoroutine(nameof(MoveToPoints));
        }
    }

    private IEnumerator MoveToPoints()
    {
        while (this.isActiveAndEnabled)
        {
            Transform currentPoint = points[pointIndex];
            while (Vector2.Distance(transform.position, currentPoint.position) > 0.1f)
            {
                transform.position = Vector2.MoveTowards(transform.position, currentPoint.position, moveSpeed * Time.deltaTime);
                yield return null;
            }

            pointIndex = (pointIndex + 1) % points.Count;
        }
    }

}
