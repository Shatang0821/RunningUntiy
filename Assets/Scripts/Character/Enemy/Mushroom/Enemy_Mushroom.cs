using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Mushroom : Enemy
{
    #region States
    public Mushroom_IdleState idleState { get; private set; }

    public Mushroom_MoveState moveState { get; private set; }

    public Mushroom_HitState hitState { get; private set; }
    #endregion

    public bool deathTrigger;

    protected override void Awake()
    {
        base.Awake();

        idleState = new Mushroom_IdleState(this, stateMachine, "Idle", this);
        moveState = new Mushroom_MoveState(this, stateMachine, "Move", this);
        hitState  = new Mushroom_HitState (this, stateMachine, "Hit" , this);
    }

    protected void Start()
    {
        stateMachine.Initialize(idleState);
    }

    private void OnEnable()
    {
        deathTrigger = false;
        stateMachine.Initialize(idleState);
    }

    public override void Die()
    {
        deathTrigger = true;
    }
}
