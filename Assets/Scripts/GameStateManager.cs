using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameState currentGameState;
    public static GameStateManager Instance;

    public int dayCount = 1;
    public static event Action<GameState> gameStateChanged;

    public enum GameState
    {
        WakingUp,
        DayStart,
        DayTasksDone,
        MeditationCutscene,
        GameOverBad,
    }

    private void Awake()
    {
        Instance = this;
    }

    public void ChangeGameState(GameState targetGameState)
    {
        currentGameState = targetGameState;
        switch (targetGameState)
        {
            case GameState.WakingUp:
                FadeManager.Instance.StartFadeFromBlack(5f);
                HelmetUIManager.Instance.TurnOffHelmetUI();
                PlayerManager.Instance.TurnOffMovement();
                break;
            case GameState.DayStart:
                break;
            case GameState.DayTasksDone:
                break;
            case GameState.MeditationCutscene:
                break;
            case GameState.GameOverBad:
                break;
        }
        gameStateChanged?.Invoke(targetGameState);
    }

    private void Start()
    {
        ChangeGameState(GameState.WakingUp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
