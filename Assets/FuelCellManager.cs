using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelCellManager : MonoBehaviour
{

    [SerializeField] private GameObject FuelCellPrefab;
    private List<GameObject> fuelCellList = new List<GameObject>();

    private void Awake()
    {
        GameStateManager.gameStateChanged += GameStateManagerGameStateChanged;
    }


    private void GameStateManagerGameStateChanged(GameStateManager.GameState state)
    {
        //turn off the helmet UI 
        if (state == GameStateManager.GameState.WakingUp)
        {
            for(int i = 0; i < fuelCellList.Count; i++)
            {
                Destroy(fuelCellList[i]);
            }
            SpawnFuelCell();
        }
        /*
        else if (state == GameStateManager.GameState.DayStart)
        {
            if (GameStateManager.Instance.dayCount == 3)
            {

            }
            else
            {

            }
        }
        else if (state == GameStateManager.GameState.DayTasksDone)
        {

        }
        else
        {


        }
        */
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void SpawnFuelCell()
    {
        fuelCellList.Clear();
        GameObject clone1 = Instantiate(FuelCellPrefab, transform, false);
        clone1.transform.localPosition = new Vector3(0f, 0f, -.5f);
        fuelCellList.Add(clone1);
        GameObject clone2 = Instantiate(FuelCellPrefab, transform, false);
        clone2.transform.localPosition = new Vector3(0f, 0f, 0.5f);
        fuelCellList.Add(clone2);
    }
}
