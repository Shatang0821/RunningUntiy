using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public IState currentState;

    protected Dictionary<System.Type, IState> stateTable;


    private void Update()
    {
        currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        currentState.PhysicUpdate();
    }

    protected void SwitchOn(IState newState)
    {
        currentState = newState;
        currentState.Enter();
    }

    public void SwitchState(IState newState)
    {
        //同じ状態であれば変換しない
        if(currentState == newState)
            return;
        currentState.Exit();
        SwitchOn(newState);
    }

    public void SwitchState(System.Type newStateType)
    {
        SwitchState(stateTable[newStateType]);
    }
    
    /// <summary>
    /// 現在状態との一致チェック
    /// </summary>
    /// <param name="state">チェック状態</param>
    /// <returns></returns>
    public bool CheckCurrentState(IState state)
    {
        return currentState != state;
    }
}