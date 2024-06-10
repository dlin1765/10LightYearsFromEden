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

    [SerializeField] private List<GameObject> UIElements, TaskList;
    [SerializeField] private GameObject TaskElements;
    [SerializeField] private GameObject ConsoleObj;

    private TextMeshProUGUI ConsoleText;
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
            Debug.Log("reset the helm text ui to text");
            UIElements[0].GetComponent<TextMeshProUGUI>().text = "Day " + GameStateManager.Instance.dayCount + ":" + "\nTasks";
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnOffHelmetUI()
    {
        helmetOn = false;
        if (HitboxScript.Instance.playerInUnsafeZone)
        {
            AlarmManager.Instance.StartAlarm(GameStateManager.Instance.dayCount < 10);
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void TurnOnHelmetUI()
    {
        helmetOn = true;
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
        }
        else
        {
            ConsoleText.text = "Console:\nProgress to Eden: " + (0 + 10 * (GameStateManager.Instance.dayCount % 10)) + "%\nTime to Destination: " + (10 - (1*(GameStateManager.Instance.dayCount%10)));
        }
        UpdateUI();
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
    }
    
}
