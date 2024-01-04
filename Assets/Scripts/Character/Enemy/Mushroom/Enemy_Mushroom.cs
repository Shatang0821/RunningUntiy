using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Mushroom : Enemy
{
    public override void Die()
    {
        base.Die();
        EventCenter.TriggerEvent("debug");
    }
}
