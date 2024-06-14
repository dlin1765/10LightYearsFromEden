using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HelmetUIManager : MonoBehaviour
{ 

    public static HelmetUIManager Instance;

    public bool consoleTask = false;
    public bool fuelTask = false;
    public bool helmetOn = false;
    public bool journalWritten = false;
    public bool helmetReturned = true;

    private bool SpecialEvent = true;
    public bool PoisonDiscovered = false;


    [SerializeField] private List<GameObject> UIElements, TaskList;
    [SerializeField] private GameObject TaskElements;
    [SerializeField] private GameObject ConsoleObj;
    [SerializeField] private GameObject VentCheck;

    private List<string> AvoidancePhrases = new List<string>();

    private TextMeshProUGUI ConsoleText;
    private ConsoleScript ConsoleUI;
    private void Awake()
    {
        Instance = this;
        GameStateManager.gameStateChanged += GameStateManagerGameStateChanged;
    }

    private void GameStateManagerGameStateChanged(GameStateManager.GameState state)
    {
        //turn off the helmet UI 
        if(state == GameStateManager.GameState.WakingUp)
        {
           
        }
        else if (state == GameStateManager.GameState.DayStart)
        {
            journalWritten = false;
            Debug.Log("reset the helm text ui to text");
            if(GameStateManager.Instance.dayCount == 3)
            {
                UIElements[0].GetComponent<TextMeshProUGUI>().text = "Day " + 23710 + ":" + "\nTasks";
                GameStateManager.Instance.HelmetDayGlitch = true;
            }
            else if(GameStateManager.Instance.SkyboxGlitchSeen)
            {
                GameStateManager.Instance.HelmetReassurances = true;
                UIElements[0].GetComponent<TextMeshProUGUI>().text = "Day " + GameStateManager.Instance.dayCount + ":" + "\nTasks\n"+AvoidancePhrases[Random.Range(0, AvoidancePhrases.Count-1)];
            }
            else
            {
                UIElements[0].GetComponent<TextMeshProUGUI>().text = "Day " + GameStateManager.Instance.dayCount + ":" + "\nTasks";
            }
            
            SetTogglesToFalse();

        }
        else if(state == GameStateManager.GameState.DayTasksDone)
        {
            // helmet prompt and journal prompt active
            TaskList[TaskList.Count - 1].SetActive(true);
            TaskList[TaskList.Count - 3].SetActive(true);
        }
        else
        {
            // move
         
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            UIElements.Add(transform.GetChild(0).GetChild(i).gameObject);
        }

        for (int i = 0; i < TaskElements.transform.GetChild(0).childCount; i++)
        {
            TaskList.Add(TaskElements.transform.GetChild(0).GetChild(i).gameObject);
        }
        ConsoleText = ConsoleObj.GetComponent<TextMeshProUGUI>();
        ConsoleUI = VentCheck.GetComponent<ConsoleScript>();

        AvoidancePhrases.Add("Everything is okay.");
        AvoidancePhrases.Add("You'll be fine.");
        AvoidancePhrases.Add("You're safe here.");
        AvoidancePhrases.Add("You just have to keep going");
        AvoidancePhrases.Add("The Helmet will keep you safe.");
        AvoidancePhrases.Add("The Helmet will keep you safe.");
        AvoidancePhrases.Add("The Helmet will keep you safe.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnOffHelmetUI()
    {
        helmetOn = false;
        AudioManager.Instance.Play("sfx_itemequip", transform, 1.0f, false);
        if (HitboxScript.Instance.playerInUnsafeZone)
        {
            AlarmManager.Instance.StartAlarm(ConsoleUI.isVentOn);
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void TurnOnHelmetUI()
    {
        helmetOn = true;
        AudioManager.Instance.Play("sfx_itemequip", transform, 1.0f, false);
        if (HitboxScript.Instance.playerInUnsafeZone)
        {
            AlarmManager.Instance.StopAlarm();
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void CompleteConsole()
    {
        consoleTask = true;
        if(ConsoleText.text.Length > 0)
        {
            ConsoleText.text = "";
            AudioManager.Instance.Play("sfx_consoleoff", transform, 1.0f, false);
        }
        else
        {
            if(GameStateManager.Instance.dayCount % 10 == 0)
            {
                GameStateManager.Instance.PlanetReset = true;
            }
            AudioManager.Instance.Play("sfx_consoleon", transform, 1.0f, false);
            ConsoleText.text = "Console:\nProgress to Eden: " + (0 + 10 * (GameStateManager.Instance.dayCount % 10)) + "%\nTime to Destination: " + (10 - (1*(GameStateManager.Instance.dayCount%10)));
        }
        UpdateUI();
        if(GameStateManager.Instance.dayCount == 7 && SpecialEvent)
        {
            SpecialEvent = false;
            GameStateManager.Instance.SkyboxGlitchSeen = true;
            GameStateManager.Instance.GlitchSkyBox();
        }
    }


    public void SetTasksNotActive()
    {
        foreach(GameObject x in UIElements)
        {
            x.SetActive(false);
        }
        foreach(GameObject y in TaskList)
        {
            y.SetActive(false);
        }
    }

    public void CompleteFuel()
    {
        fuelTask = true;
        UpdateUI();
    }

    public void TurnOffDayOverTasks()
    {
        TaskList[TaskList.Count - 3].SetActive(false);
        TaskList[TaskList.Count - 2].SetActive(false);
        TaskList[TaskList.Count - 1].SetActive(false);
    }

    public void WriteInJournal()
    {
        journalWritten = true;
        AudioManager.Instance.Play("sfx_scribble", transform, 1.0f, false);
        TaskList[TaskList.Count - 1].SetActive(false);
        if (journalWritten && helmetReturned)
        {
            TaskList[TaskList.Count - 2].SetActive(true);
        }
    }

    public void GoToHibernation()
    {
        TurnOffDayOverTasks();
        ResetTasks();
        GameStateManager.Instance.ChangeGameState(GameStateManager.GameState.SleepTime);
    }

    public void HelmetReturned()
    {
        helmetReturned = true;
        if(GameStateManager.Instance.currentGameState == GameStateManager.GameState.DayTasksDone)
        {
            TaskList[TaskList.Count - 3].SetActive(false);
            if (journalWritten && helmetReturned)
            {
                TaskList[TaskList.Count - 2].SetActive(true);
            }
        }
    }

    public void HelmetTaken()
    {
        helmetReturned = false;
        if(GameStateManager.Instance.currentGameState == GameStateManager.GameState.DayTasksDone)
        {
            TaskList[TaskList.Count - 3].SetActive(true);
        }
    }

    public void MeditationPressed()
    {
        GameStateManager.Instance.ChangeGameState(GameStateManager.GameState.MeditationCutscene);
    }

    public void SetTogglesToFalse()
    {
        UIElements[1].GetComponent<Toggle>().isOn = false;
        UIElements[2].GetComponent<Toggle>().isOn = false;
    }

    private void ResetTasks()
    {
        ConsoleText.text = "";
        consoleTask = false;
        fuelTask = false;
    }

    public void UpdateUI()
    {
        UIElements[1].GetComponent<Toggle>().isOn = consoleTask;
        UIElements[2].GetComponent<Toggle>().isOn = fuelTask;
        if(consoleTask && fuelTask)
        {
            if(GameStateManager.Instance.currentGameState != GameStateManager.GameState.DayTasksDone)
            {
                GameStateManager.Instance.ChangeGameState(GameStateManager.GameState.DayTasksDone);
                UIElements[0].GetComponent<TextMeshProUGUI>().text = "Day " + GameStateManager.Instance.dayCount + ":" + "\nTasks done! Return helmet and head back to hibernation";
            }
        }
    }

    public void SetMeditationActive()
    {
        TaskList[TaskList.Count - 4].SetActive(true);
        AudioManager.Instance.Play("sfx_meditation", transform, 1.0f, false);
    }
    
}
