using System.Collections.Generic;
using UnityEngine;


public class MushroomStateMachine : EnemyStateMachine
{
    private void Start()
    {
        // 初期状態（アイドル状態）に切り替え
        SwitchOn(stateTable[typeof(Mushroom_IdleState)]);
    }

    private void OnEnable()
    {
        // 初期状態（アイドル状態）に切り替え
        SwitchOn(stateTable[typeof(Mushroom_IdleState)]);
    }
}