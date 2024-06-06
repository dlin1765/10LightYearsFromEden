using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameState currentGameState;
    public static GameStateManager Instance;

    public int dayCount = 0;
    public static event Action<GameState> gameStateChanged;
    private Vector3 WakeUpPos = new Vector3(0f, 0f, 0f);

    public enum GameState
    {
        WakingUp,
        DayStart,
        DayTasksDone,
        SleepTime,
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
                
                PlayerManager.Instance.transform.position = WakeUpPos;
                FadeManager.Instance.StartFadeFromBlack(5f);
                //HelmetUIManager.Instance.TurnOffHelmetUI();
                PlayerManager.Instance.TurnOffMovement();
                dayCount = dayCount + 1;
                //HelmetUIManager.Instance.TurnOffDayOverTasks();
                break;
            case GameState.DayStart:
                PlayerManager.Instance.TurnOnMovement();
                break;
            case GameState.DayTasksDone:
                break;
            case GameState.SleepTime:
                FadeManager.Instance.StartFadeToBlack(7f);
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
