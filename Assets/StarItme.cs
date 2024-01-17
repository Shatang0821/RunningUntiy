using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarItme : MonoBehaviour
{
    Animator animator;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.dashItemGet = true;
            Debug.Log(player.dashItemGet);
            
            animator.SetTrigger("Get");
        }
    }
}
