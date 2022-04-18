using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateGameStates(GameState.ShowMenu);
    }

    // uses state machine
    public void UpdateGameStates(GameState newState)
    {
        State = newState;
        switch (newState)
        {
            case GameState.ShowMenu:
                State = GameState.ShowMenu;
                break;
            case GameState.PlayerPlay:
                State = GameState.PlayerPlay;
                break;
            case GameState.PlayerDeath:
                State = GameState.PlayerDeath;
                break;
            case GameState.LevelStart:
                State = GameState.LevelStart;
                break;
            case GameState.LevelCompleted:
                State = GameState.LevelCompleted;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }
}

public enum GameState
{
    // showing menu currently state
    ShowMenu,

    // player can either be playing or dead
    PlayerPlay,
    PlayerDeath,

    // level completed successfully or started
    LevelCompleted,
    LevelStart
}