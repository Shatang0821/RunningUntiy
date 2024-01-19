using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [Header("Jump Info")]
    [SerializeField] private float jumpForce;
    [Header("Audio Info")]
    [SerializeField] private AudioData jumpSFX;
    Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            animator.SetTrigger("Jump");
            AudioManager.Instance.PlaySFX(jumpSFX);
            PlayerStateMachine playerStateMachine = collision.gameObject.GetComponent<PlayerStateMachine>();
            if (playerStateMachine != null)
            {
                playerStateMachine.ForceJumpStateChange(jumpForce);
            }
        }
    }
}
