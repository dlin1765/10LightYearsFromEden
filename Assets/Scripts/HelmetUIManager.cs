using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelmetUIManager : MonoBehaviour
{ 

    public static HelmetUIManager Instance;

    private void Awake()
    {
        Instance = this;
        GameStateManager.gameStateChanged += GameStateManagerGameStateChanged;
    }

    private void GameStateManagerGameStateChanged(GameStateManager.GameState state)
    {
        //turn off the helmet UI 
        if (state != GameStateManager.GameState.WakingUp)
        {
            //TurnOffHelmetUI();
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
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
