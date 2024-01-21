using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedBall : MonoBehaviour
{
    [SerializeField] private Transform centerPoint; // �~�^���̒��S
    [SerializeField] private GameObject chainPrefab; // �`�F�C���̃v���n�u
    [SerializeField] private GameObject ballPrefab; // �X�p�C�N�{�[���̃v���n�u

    [SerializeField] private List<GameObject> chains;

    [SerializeField] private int numberOfChains;
                     //private float baseRadius = 0.5f; // ��{�ƂȂ锼�a
    [SerializeField] private float chainSpacing = 0.5f; // �`�F�C���Ԃ̋���

    [SerializeField] private float speed = 1f; // �~�^���̑��x
    [SerializeField] private float startAngle = 0f;
    [SerializeField] private float endAngle = 180f;

    private float currentAngle = 0f;
    private bool movingForward = true; // �ړ�������ǐՂ���t���O

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
        // �����p�x�����W�A���ɕϊ����܂�
        float angle = startAngle * Mathf.Deg2Rad;

        for (int i = 0; i < chains.Count; i++)
        {
            // �`�F�C�����Ƃ̔��a���v�Z���܂�
            float chainRadius = chainSpacing * i;

            // �`�F�C���̈ʒu���v�Z���܂�
            Vector3 chainPosition = centerPoint.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * chainRadius;

            // �`�F�C���̈ʒu��ݒ肵�܂�
            chains[i].transform.position = chainPosition;
        }
        // �{�[���̈ʒu��ݒ肵�܂��B�{�[���͂��ׂẴ`�F�C���̖����ɔz�u����܂�
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
        //�`�F�C�������
        GameObject newChain = Instantiate(chainPrefab, this.transform);
        //newChain.transform.localPosition = new Vector3(0, -chainSpacing * chains.Count, 0);
        chains.Add(newChain);
    }
}
