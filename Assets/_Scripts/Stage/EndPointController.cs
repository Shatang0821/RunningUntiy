    using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndPointController : MonoBehaviour
{
    [SerializeField] GameObject collectVFX; //�G�t�F�N�g

    Transform player;       //�v���C���[�ʒu
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�v���C���[�ɐG����
        if(collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject.GetComponent<Transform>();
            StartCoroutine(nameof(WaitForAnimation));
            animator.SetTrigger("Get");
        }
    }

    IEnumerator WaitForAnimation()
    {
        //�A�j���[�V�������I���̂�҂�
        yield return new WaitForSeconds(1.5f);
        PoolManager.Release(collectVFX, transform.position);
        
        yield return new WaitForSeconds(0.5f);
        
        GameClear();
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// �N���A����
    /// </summary>
    private void GameClear()
    {
        EventCenter.TriggerEvent(TimeEvents.StartTime);
        EventCenter.TriggerEvent(InputEvents.FixedInput);
        //�N���A������X�e�[�W�I�ԃV�[���ɖ߂�
        SceneLoader.Instance.LoadStageSelectScene();
    }
}
