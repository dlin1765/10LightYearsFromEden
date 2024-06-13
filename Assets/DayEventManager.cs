using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayEventManager : MonoBehaviour
{
    private GameObject SlidingDoor, LooseWall, Plant, Speaker;
   
    private Vector3 closedPosition = new Vector3(0f, 0f, 0f);
    private Vector3 openPosition = new Vector3(1.793483f, 0f, 0f);

    private bool PoisonDiscovered = false;

    private void Awake()
    {
        GameStateManager.gameStateChanged += GameStateManagerGameStateChanged;
    }

    private void GameStateManagerGameStateChanged(GameStateManager.GameState state)
    {
        if (state == GameStateManager.GameState.WakingUp)
        {
            if (GameStateManager.Instance.dayCount == 5)
            {
                SlidingDoor.transform.localPosition = openPosition;
                Plant.transform.GetChild(0).localPosition = new Vector3(0, 0.5f, 1f);
                Plant.transform.Rotate(Vector3.right * 90f, Space.Self);
                Plant.transform.localPosition = Plant.transform.localPosition + new Vector3(0f, 0.5f, 0f);
            }
            else
            {
                SlidingDoor.transform.localPosition = closedPosition;
            }
            if(GameStateManager.Instance.dayCount > 5)
            {
                if (!PoisonDiscovered)
                {
                    LooseWall.transform.localPosition = LooseWall.transform.localPosition + new Vector3(0f, 0f, -0.015f);
                    LooseWall.transform.Rotate(Vector3.right * -1f, Space.Self);
                }
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        LooseWall = transform.GetChild(0).gameObject;
        Plant = transform.GetChild(1).gameObject;
        SlidingDoor = transform.GetChild(2).GetChild(0).gameObject;
        Speaker = transform.GetChild(3).gameObject;
    }

    public void LooseDoorTaken()
    {
        StartCoroutine(DestroyAfterABit(LooseWall));
        HelmetUIManager.Instance.PoisonDiscovered = true;
    }

    private IEnumerator DestroyAfterABit(GameObject obj)
    {
        PoisonDiscovered = true;
        yield return new WaitForSeconds(5f);
        Destroy(obj);
    }
}
