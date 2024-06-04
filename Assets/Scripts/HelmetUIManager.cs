using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelmetUIManager : MonoBehaviour
{ 

    public static HelmetUIManager Instance;

    public bool consoleTask = false;
    public bool fuelTask = false;
    public bool helmetOn = false;

    private void Awake()
    {
        Instance = this;
        GameStateManager.gameStateChanged += GameStateManagerGameStateChanged;
    }

    private void GameStateManagerGameStateChanged(GameStateManager.GameState state)
    {
        //turn off the helmet UI 
        if (state == GameStateManager.GameState.WakingUp)
        {

        }
        else
        {
            // move
         
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnOffHelmetUI()
    {
        helmetOn = false;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void TurnOnHelmetUI()
    {
        helmetOn = true;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void CompleteConsole()
    {
        consoleTask = true;
    }
    
    public void CompleteFuel()
    {
        fuelTask = true;
    }
}
