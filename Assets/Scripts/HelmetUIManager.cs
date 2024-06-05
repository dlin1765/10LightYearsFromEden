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

    [SerializeField] private List<GameObject> UIElements;

    private void Awake()
    {
        Instance = this;
        GameStateManager.gameStateChanged += GameStateManagerGameStateChanged;
    }

    private void GameStateManagerGameStateChanged(GameStateManager.GameState state)
    {
        //turn off the helmet UI 
        if (state == GameStateManager.GameState.DayStart)
        {
            Debug.Log("reset the helm text ui to text");
            UIElements[0].GetComponent<TextMeshProUGUI>().text = "Tasks";
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
        UpdateUI();
    }
    
    public void CompleteFuel()
    {
        fuelTask = true;
        UpdateUI();
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
                UIElements[0].GetComponent<TextMeshProUGUI>().text = "Tasks done! Head back to the Cryopod!";
            }
        }
    }
    
}
