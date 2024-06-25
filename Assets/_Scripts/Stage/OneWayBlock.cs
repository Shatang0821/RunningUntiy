using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayBlock : MonoBehaviour
{
    Collider2D childCollider;
    [Header("Jump Info")]
    [SerializeField] private float jumpForce;       //�Е��ړ��\�v���b�g�t�H�[��
    [SerializeField] private float waitSeconds;     //�v���C���[�����蔲���܂ł̎���
    WaitForSeconds waitTime;
    private void Awake()
    {
        waitTime = new WaitForSeconds(waitSeconds);
        childCollider = transform.GetChild(0).GetComponent<Collider2D>();
        if (childCollider == null)
        {
            Debug.LogWarning("not found collider2D");
        }
        else
        {
            childCollider.enabled = true;
        }
    }

    private void OnEnable()
    {
        childCollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //Player�̏�Ԃ��J�X�^���W�����v�ɋ����J�ڂ�����
            childCollider.enabled = false;
            PlayerStateMachine playerStateMachine = collision.gameObject.GetComponent<PlayerStateMachine>();
            if(playerStateMachine != null )
            {
                playerStateMachine.ForceJumpStateChange(jumpForce);
            }
            if(childCollider!=null)
                childCollider.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && childCollider!= null)
        {
            StartCoroutine(EnableCollider(childCollider));
        }
    }

    /// <summary>
    /// �����蔻�����莞�Ԍo�Ă���L���ɂ���
    /// </summary>
    /// <param name="collision">�L���ɂ���R���C�_�[</param>
    /// <returns></returns>
    IEnumerator EnableCollider(Collider2D collision)
    { 
        yield return waitTime;
        collision.enabled = true;
    }
}
