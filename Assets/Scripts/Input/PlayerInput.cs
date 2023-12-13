using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Input", fileName = "Player Input")]
public class PlayerInput : ScriptableObject
{
    private InputActions inputActions;

    public Vector2 Axis =>inputActions.GamePlay.Axis.ReadValue<Vector2>();


    public bool Jump => inputActions.GamePlay.Jump.WasPerformedThisFrame();
    public bool StopJump => inputActions.GamePlay.Jump.WasReleasedThisFrame();

    public bool Dash => inputActions.GamePlay.Dash.WasPerformedThisFrame();
    private void OnEnable()
    {
        inputActions = new InputActions();

        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
}
