using System.Collections.Generic;
using UnityEngine;


public class MushroomStateMachine : EnemyStateMachine
{
    private void Start()
    {
        // ������ԁi�A�C�h����ԁj�ɐ؂�ւ�
        SwitchOn(stateTable[typeof(Mushroom_IdleState)]);
    }

    private void OnEnable()
    {
        EventCenter.Subscribe("debug", DebugString);
        // ������ԁi�A�C�h����ԁj�ɐ؂�ւ�
        SwitchOn(stateTable[typeof(Mushroom_IdleState)]);
    }

    private void OnDisable()
    {
        EventCenter.Unsubscribe("debug", DebugString);
    }

    void DebugString()
    {
        Debug.Log("Test");
    }
}