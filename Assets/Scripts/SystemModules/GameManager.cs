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
        
    }

    private void Update()
    {
        if (GameState == GameState.Initialize)
        {
            GameState = GameState.Respawn;
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
