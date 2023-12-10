using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Input", fileName = "Player Input")]
public class PlayerInput : ScriptableObject
{
    private InputActions inputActions;

    public float AxisX =>inputActions.GamePlay.AxisX.ReadValue<float>();
    public float AxisY =>inputActions.GamePlay.AxisY.ReadValue<float>();

    public Vector2 Axis => new(AxisX, AxisY);

    public bool Jump => inputActions.GamePlay.Jump.WasPerformedThisFrame();

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
