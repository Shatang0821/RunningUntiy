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
        Application.targetFrameRate = 144;
        
    }

    private void Update()
    {
        switch (gameState)
        {
            case GameState.Initialize:
                break;
            case GameState.Playing:
                break;
            case GameState.Respawn:
                break;
            case GameState.Paused:
                break;
            default:
                break;
        }
    }
}

public enum GameState
{
    Initialize,
    Playing,
    Respawn,
    Paused,
    MainMenu,
    GameClear,
}
