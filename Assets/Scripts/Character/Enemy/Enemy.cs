using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    //[SerializeField] protected LayerMask whatIsPlayer;

    //[Header("Attack info")]
    //public float attackDistance;

    [Header("Move info")]
    public float moveSpeed;
    public float idleTime;



    //public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, 50, whatIsPlayer);

    //protected override void OnDrawGizmos()
    //{
    //    base.OnDrawGizmos();

    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * facingDir, transform.position.y));
    //}
}
