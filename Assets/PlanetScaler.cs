using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetScaler : MonoBehaviour
{

    private void Awake()
    {
        GameStateManager.gameStateChanged += GameStateManagerGameStateChanged;
    }
    // day 1 = 0 day 1 = 1 day 3 = 2 
    
    private void GameStateManagerGameStateChanged(GameStateManager.GameState state)
    {
        //turn off the helmet UI 
        if (state == GameStateManager.GameState.WakingUp)
        {
            int day = GameStateManager.Instance.dayCount;
            if (day % 10 == 0)
            {
                transform.localScale.Set(1f, 1f, 1f);
            }
            else
            {
                transform.localScale.Set(1f + 0.5f * (day - 1), 1f + 0.5f * (day - 1), 1f + 0.5f * (day - 1));
            }
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
}
