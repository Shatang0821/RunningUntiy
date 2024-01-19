    using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndPointController : MonoBehaviour
{
    [SerializeField] GameObject collectVFX; //�G�t�F�N�g

    Transform player;       //�v���C���[�ʒu
    Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player = collision.gameObject.GetComponent<Transform>();
            StartCoroutine(nameof(MoveToPlayer));
            animator.SetTrigger("Get");
        }
    }

    IEnumerator MoveToPlayer()
    {
        while(Vector2.Distance(transform.position,player.position)>0.01f)
        {
            // �G���h�|�C���g�̈ʒu���������v���C���[�ɒǐ�������
            transform.position = Vector3.Lerp(transform.position, player.position, 5 * Time.deltaTime);
            yield return null;
        }
        PoolManager.Release(collectVFX, transform.position);
        
        yield return new WaitForSeconds(0.5f);
        GameClear();
        this.gameObject.SetActive(false);
    }

    private void GameClear()
    {
        EventCenter.TriggerEvent(TimeEvents.StartTime);
        EventCenter.TriggerEvent(InputEvents.FixedInput);
        SceneLoader.Instance.LoadGameClearScene();
    }
}
