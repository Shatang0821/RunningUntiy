using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PersistentSingleton<GameManager>
{
    public static GameState GameState { get => Instance.gameState; set => Instance.gameState = value; }

    [SerializeField] GameState gameState = GameState.Initialize;

    protected override void Awake()
    {
        base.Awake();
        if(GameState == GameState.Initialize)
        {
            EventCenter.TriggerEvent(EventNames.Respawn);
        }
    }
}

public enum GameState
{
    Initialize,
    Playing,
    Respawn,
    Paused,
}
