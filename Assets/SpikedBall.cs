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
    public float lerpSpeed = 0.1f; // ��Ԃ̑��x
    private float currentAngle = 0f;
    private bool movingForward = true; // �ړ�������ǐՂ���t���O

    private void Start()
    {
        currentAngle = 0;
    }

    void Update()
    {
        // ���݂̈ړ������Ɋ�Â��Ċp�x���X�V
        if (movingForward)
        {
            currentAngle += speed * Time.deltaTime;
        }
        else
        {
            currentAngle -= speed * Time.deltaTime;
        }

        // �p�x���n�_�ƏI�_�̊Ԃɐ���
        currentAngle = Mathf.Clamp(currentAngle, startAngle, endAngle);

        // �V�����ʒu���v�Z
        Vector2 newPosition = centerPoint.position - new Vector3(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad), 0) * radius;

        // �I�u�W�F�N�g�̈ʒu���X�V
        transform.position = newPosition;

        // �n�_�܂��͏I�_�ɒB�����������؂�ւ�
        if (currentAngle <= startAngle || currentAngle >= endAngle)
        {
            movingForward = !movingForward;
        }
    }

    void OnDrawGizmos()
    {
        if (centerPoint == null)
            return;

        Gizmos.color = Color.red; // �M�Y���̐F��ݒ�

        Vector3 start = centerPoint.position + new Vector3(Mathf.Cos(startAngle * Mathf.Deg2Rad), Mathf.Sin(startAngle * Mathf.Deg2Rad), 0) * radius;
        Vector3 end = centerPoint.position + new Vector3(Mathf.Cos(endAngle * Mathf.Deg2Rad), Mathf.Sin(endAngle * Mathf.Deg2Rad), 0) * radius;

        // �n�_����I�_�܂ł̋O����`��
        float step = 0.1f;
        for (float angle = startAngle; angle <= endAngle; angle += step)
        {
            Vector3 previous = centerPoint.position - new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0) * radius;
            Vector3 next = centerPoint.position - new Vector3(Mathf.Cos((angle + step) * Mathf.Deg2Rad), Mathf.Sin((angle + step) * Mathf.Deg2Rad), 0) * radius;

            Gizmos.DrawLine(previous, next);
        }

        // �n�_�ƏI�_������
        Gizmos.DrawSphere(start, 0.2f);
        Gizmos.DrawSphere(end, 0.2f);
    }
}
