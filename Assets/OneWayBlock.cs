using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayBlock : MonoBehaviour
{
    Collider2D childCollider;

    private void Awake()
    {
        childCollider = transform.GetChild(0).GetComponent<Collider2D>();
        if (childCollider == null)
        {
            Debug.LogWarning("not found collider2D");
        }
        else
        {
            childCollider.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerStateMachine playerStateMachine = collision.gameObject.GetComponent<PlayerStateMachine>();
            if(playerStateMachine != null )
            {
                playerStateMachine.ForceJumpStateChange();
            }

            childCollider.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            childCollider.enabled = true;
        }
    }
}
