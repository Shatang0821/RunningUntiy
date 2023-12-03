using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [SerializeField] PlayerState[] states;

    Player player;

    private void Awake()
    {
        player = GetComponent<Player>();

        stateTable = new Dictionary<System.Type,IState>(states.Length);

        foreach(PlayerState state in states)
        {
            state.Initialize(player, this);
            stateTable.Add(state.GetType(), state);
        }
    }

    private void Start()
    {
        SwitchOn(stateTable[typeof(PlayerIdleState)]);
    }
}
