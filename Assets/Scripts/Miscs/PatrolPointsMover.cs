using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;




public class PatrolPointsMover : MonoBehaviour
{
    private enum PatrolType
    {
        Loop,     // 1234, 1234...
        PingPong  // 1234321, 1234321...
    }
    [Header("Move Info")]
    [SerializeField] private PatrolType patrolType; // �񋓌^���g��

    [SerializeField] public List<Transform> points;
    [SerializeField] private float moveSpeed;
    
    private int pointIndex = 0;
    private bool isGoingForward = true; //�|�C���g�Ɍ������đO�i���Ă��邩��ǐ�

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

            UpdatePointIndex();
        }
    }

    /// <summary>
    /// �����p�^�[������
    /// </summary>
    private void UpdatePointIndex()
    {
        switch (patrolType)
        {
            case PatrolType.Loop:
                pointIndex = (pointIndex + 1) % points.Count;
                break;

            case PatrolType.PingPong:
                // pointIndex �������܂��͌������邩������
                if ((pointIndex >= points.Count - 1 && isGoingForward) || (pointIndex <= 0 && !isGoingForward))
                {
                    isGoingForward = !isGoingForward; // �����𔽓]
                }
                break;
        }
    }

}
