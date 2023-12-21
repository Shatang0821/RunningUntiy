using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    IState currentState;

    protected Dictionary<System.Type, IState> stateTable;

    void OnGUI()
    {
        Rect rect = new Rect(200, 150, 200, 200);
        string message = currentState.ToString();
        GUIStyle style = new GUIStyle();
        style.fontSize = 50;
        style.fontStyle = FontStyle.Bold;
        GUI.Label(rect, message, style);
    }
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
        currentState.Exit();
        SwitchOn(newState);
    }

    public void SwitchState(System.Type newStateType)
    {
        SwitchState(stateTable[newStateType]);
    }
}