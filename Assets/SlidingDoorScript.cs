using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoorScript : MonoBehaviour
{
    private Vector3 closedPosition = new Vector3(0f, 0f, 0f);
    private Vector3 openPosition = new Vector3(1.793483f, 0f, 0f);

    private GameObject DoorObject;

    private void Awake()
    {
        GameStateManager.gameStateChanged += GameStateManagerGameStateChanged;
    }

    private void GameStateManagerGameStateChanged(GameStateManager.GameState state)
    {
        if(state == GameStateManager.GameState.WakingUp)
        {
            if(GameStateManager.Instance.dayCount == 5)
            {
                DoorObject.transform.localPosition = openPosition;
            }
            else
            {
                DoorObject.transform.localPosition = closedPosition;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        DoorObject = transform.GetChild(0).gameObject;
    }

}
