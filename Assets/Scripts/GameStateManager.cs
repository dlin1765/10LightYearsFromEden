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
    private Vector3 WakeUpPos = new Vector3(0f, 0f, -1f);
    public GameObject FakeSkybox;

    [SerializeField] private GameObject explosionShip;
 

    public enum GameState
    {
        WakingUp,
        DayStart,
        DayTasksDone,
        SleepTime,
        MeditationCutscene,
        FinalCutscene,
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
                StartCoroutine(MeditationCutscene());
                break;
            case GameState.FinalCutscene:
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

    private IEnumerator MeditationCutscene()
    {
        // fade halfway
        FadeManager.Instance.halfFade();
        PlayerManager.Instance.TurnOffMovement();
        PlayerManager.Instance.StartFloating();
        HelmetUIManager.Instance.SetTasksNotActive();
        yield return new WaitForSeconds(10f);
        Debug.Log("EXPLOSION");
        explosionShip.SetActive(true);
        FadeManager.Instance.OtherHalfFade();
        yield return new WaitForSeconds(10f);
        PlayerManager.Instance.SetPlayerFinal();
        ChangeGameState(GameState.FinalCutscene);
        FadeManager.Instance.StartFadeFromBlack(5f);
        yield return new WaitForSeconds(5f);
        PlanetScaler.Instance.StartPlanetFade();
        yield return new WaitForSeconds(10f);
        FakeSkybox.GetComponent<FadeSkyBox>().StartFadeOutSkybox();

        // lower volume of sounds
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GlitchSkyBox()
    {
        StartCoroutine(StartGlitch());
    }

    private IEnumerator StartGlitch()
    {
        AlarmManager.Instance.StartLightsOff(0f, true);
        // play power down sound
        yield return new WaitForSeconds(3f);
        // play helmet glitch sound 
        int i = 0;
        while(i < 10)
        {
            i += 1;
            FakeSkybox.SetActive(false);
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, 0.3f));
            FakeSkybox.SetActive(true);
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, 0.3f));
        }
        AlarmManager.Instance.StartLightsOff(0f, false);
        //stop helmet glitch sound and play power up sound
    }
}
