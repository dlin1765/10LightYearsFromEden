using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static AlarmManager Instance;
    public bool alarmHappening = false;
    [SerializeField] private List<LightFlash> LightObjects;

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
        if (!alarmHappening)
        {
            alarmHappening = true;
            foreach (LightFlash x in LightObjects)
            {
                x.AlarmMode();
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
            HelmetUIManager.Instance.SetMeditationActive();
        }
    }

    public void StopAlarm()
    {
        if (alarmHappening)
        {
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

    private IEnumerator LightsOff(float duration, bool off = true)
    {
        foreach (LightFlash x in LightObjects)
        {
            x.LightOffOrOn(off);
        }
        yield return new WaitForSeconds(duration);
    }
}
