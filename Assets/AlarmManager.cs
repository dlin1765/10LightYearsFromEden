using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static AlarmManager Instance;
    public bool alarmHappening = false;
    [SerializeField] private List<LightFlash> LightObjects;
    GameObject currentLoop = null;
    GameObject loop = null;
    private Coroutine AlarmRoutine = null;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            LightObjects.Add(transform.GetChild(i).GetComponent<LightFlash>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartAlarm(bool isLethal)
    {
        Debug.Log("started alarm");
        if (!alarmHappening)
        {
            alarmHappening = true;
            foreach (LightFlash x in LightObjects)
            {
                x.AlarmMode();
            }
            currentLoop = AudioManager.Instance.LerpLoopable("sfx_normalalarm", transform, 0.0f); // copy paste this line when you want a loopable sound to play 
            if (isLethal)
            {
                AudioManager.Instance.Play("sfx_gashissing", transform.GetChild(3), 1.0f, false);
            }
            AlarmRoutine = StartCoroutine(AlarmFlashing(isLethal));
        }
    }

    private IEnumerator AlarmFlashing(bool isLethal)
    {
        float timer = 0f;
        while(timer <= 10f)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        yield return null;
        if (isLethal)
        {
            Debug.Log("player Lost");
            GameStateManager.Instance.ChangeGameState(GameStateManager.GameState.GameOverBad);
        }
        else
        {
            // start extra audio here 
            StartCoroutine(currentLoop.GetComponent<Loopable>().LerpDestroySelf(0.0f, 0.1f));
            currentLoop = AudioManager.Instance.LerpLoopable("sfx_alarm", transform, 0.0f);
            loop = AudioManager.Instance.LerpLoopable("sfx_electricalfailure", transform, 0.0f);
            HelmetUIManager.Instance.SetMeditationActive();
        }
    }

    public void StopAlarm()
    {
        if (alarmHappening)
        {
            StartCoroutine(currentLoop.GetComponent<Loopable>().LerpDestroySelf(0.0f, 0.1f));
            alarmHappening = false;
            foreach (LightFlash x in LightObjects)
            {
                x.StopAlarmMode();
            }
            StopCoroutine(AlarmRoutine);
        }
    }

    public Coroutine StartLightsOff(float duration, bool off = true)
    {
        Coroutine a = StartCoroutine(LightsOff(duration, off));
        return a;
    }

    public void TurnOffAudios()
    {
        StartCoroutine(currentLoop.GetComponent<Loopable>().LerpDestroySelf(0.0f, 0.0f));
        StartCoroutine(loop.GetComponent<Loopable>().LerpDestroySelf(0.0f, 0.0f));
    }

    private IEnumerator LightsOff(float duration, bool off = true)
    {
        foreach (LightFlash x in LightObjects)
        {
            x.LightOffOrOn(off);
        }
        yield return new WaitForSeconds(duration);
    }
}
