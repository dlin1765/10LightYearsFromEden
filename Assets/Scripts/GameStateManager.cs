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

    public bool FuelCellDoorHint = false;
    public bool GeigarCounterDiscovered = false;
    public bool SeenHallucination = false;
    public bool SkyboxGlitchSeen = false;
    public bool ClickedSystemButton = false;
    public bool HelmetReassurances = false;
    public bool HelmetDayGlitch = false;
    public bool PlanetReset = false;

    GameObject currentLoop = null;
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
                HitboxScript.Instance.gameObject.SetActive(false);
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
        TurnOnShipSound();
    }

    public void TurnOffShipSound()
    {
        StartCoroutine(currentLoop.GetComponent<Loopable>().LerpDestroySelf(0.0f, 0.0f));
    }
    public void TurnOnShipSound()
    {
        currentLoop = AudioManager.Instance.LerpLoopable("sfx_shipambience", PlayerManager.Instance.transform, 0.5f);
    }

    private IEnumerator MeditationCutscene()
    {
        // fade halfway
        FadeManager.Instance.halfFade();
        PlayerManager.Instance.TurnOffMovement();
        PlayerManager.Instance.StartFloating();
        HelmetUIManager.Instance.SetTasksNotActive();
        float duration = AudioManager.Instance.Play("sfx_Meditation1", PlayerManager.Instance.transform, 1.0f, true);
        yield return new WaitForSeconds(duration);
        duration = AudioManager.Instance.Play("sfx_MeditationWarning1", PlayerManager.Instance.transform, 1.0f, true);
        yield return new WaitForSeconds(duration);
        duration = AudioManager.Instance.Play("sfx_Meditation2", PlayerManager.Instance.transform, 1.0f, true);
        yield return new WaitForSeconds(duration);
        duration = AudioManager.Instance.Play("sfx_MeditationWarning2", PlayerManager.Instance.transform, 1.0f, true);
        yield return new WaitForSeconds(duration);
        duration = AudioManager.Instance.Play("sfx_Meditation3", PlayerManager.Instance.transform, 1.0f, true);
        GameObject loop = AudioManager.Instance.LerpLoopable("sfx_explosioncoming", PlayerManager.Instance.transform, 3f);
        yield return new WaitForSeconds(duration);
        duration = AudioManager.Instance.Play("sfx_Meditation4", PlayerManager.Instance.transform, 1.0f, true);
        GameObject loop2 = AudioManager.Instance.LerpLoopable("sfx_MeditationWarningLoop", PlayerManager.Instance.transform, 3f);
        yield return new WaitForSeconds(duration);
        duration = AudioManager.Instance.Play("sfx_MeditationWarning3", PlayerManager.Instance.transform, 1.0f, true);
        yield return new WaitForSeconds(duration);
        duration = AudioManager.Instance.Play("sfx_MeditationWarningCount", PlayerManager.Instance.transform, 1.0f, true);
        //GameObject elecLoop = AudioManager.Instance.LerpLoopable("sfx_electricalfailure", PlayerManager.Instance.transform, 1.0f);
        yield return new WaitForSeconds(10f);
        StartCoroutine(currentLoop.GetComponent<Loopable>().LerpDestroySelf(0.0f, 0.0f));
        StartCoroutine(loop.GetComponent<Loopable>().LerpDestroySelf(0.0f, 0.0f));
        StartCoroutine(loop2.GetComponent<Loopable>().LerpDestroySelf(0.0f, 0.0f));
        AlarmManager.Instance.TurnOffAudios();
        explosionShip.SetActive(true);
        FadeManager.Instance.OtherHalfFade();
        yield return new WaitForSeconds(9f);
        explosionShip.SetActive(false);
        yield return new WaitForSeconds(1f);
        PlayerManager.Instance.SetPlayerFinal();
        ChangeGameState(GameState.FinalCutscene);
        FadeManager.Instance.StartFadeFromBlack(5f);
        GameObject loop3 = AudioManager.Instance.LerpLoopable("sfx_spaceambience", PlayerManager.Instance.transform, 3f);
        yield return new WaitForSeconds(5f);
        PlanetScaler.Instance.StartPlanetFade();
        yield return new WaitForSeconds(10f);
        FakeSkybox.GetComponent<FadeSkyBox>().StartFadeOutSkybox();
        StartCoroutine(loop3.GetComponent<Loopable>().LerpDestroySelf(0.0f, 0.0f));
        GameObject loop4 = AudioManager.Instance.LerpLoopable("sfx_music", PlayerManager.Instance.transform, 3f);
        yield return new WaitForSeconds(5f);
        // lower volume of sounds
    }

    public void PlayVoiceline(string name)
    {

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
        TurnOffShipSound();
        AudioManager.Instance.Play("sfx_powerdown", PlayerManager.Instance.transform, 1.0f, false);
        yield return new WaitForSeconds(3f);
        // play helmet glitch sound 
        GameObject otherLoop = AudioManager.Instance.LerpLoopable("sfx_helmetglitching", PlayerManager.Instance.transform, 1.0f);
        int i = 0;
        while(i < 10)
        {
            i += 1;
            FakeSkybox.SetActive(false);
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, 0.3f));
            FakeSkybox.SetActive(true);
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, 0.3f));
        }
        StartCoroutine(otherLoop.GetComponent<Loopable>().LerpDestroySelf(0.0f, 0.0f));
        AlarmManager.Instance.StartLightsOff(0f, false);
        TurnOnShipSound();
        AudioManager.Instance.Play("sfx_lightsflickeron", PlayerManager.Instance.transform, 1.0f, false);
        //stop helmet glitch sound and play power up sound
    }
}
