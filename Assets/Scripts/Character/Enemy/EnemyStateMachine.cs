using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    [SerializeField] EnemyState[] states;

    Enemy enemy;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();

        stateTable = new Dictionary<System.Type, IState>(states.Length);

        foreach(var state in states)
        {
            state.Initialize(enemy, this);
            stateTable.Add(state.GetType(), state);
        }
    }
}
