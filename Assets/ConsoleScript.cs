using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConsoleScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private List<GameObject> UIElements;

    private GameObject consoleCanvas;
    private bool SystemOrNav = true;

    public bool isVentOn = true;
    public bool thrustersOn = true;
    public bool heatOn = true;
    void Start()
    {
        consoleCanvas = transform.GetChild(0).gameObject;
        for (int i = 0; i < consoleCanvas.transform.childCount; i++)
        {
            UIElements.Add(consoleCanvas.transform.GetChild(i).gameObject);
        }
    }

    public void TurnOnConsole()
    {
        SystemOrNav = true;
        UIElements[0].SetActive(true);
        UIElements[0].transform.GetComponentInChildren<TextMeshProUGUI>().text = "Systems";

        for (int i = 2; i < UIElements.Count; i++)
        {
            UIElements[i].SetActive(false);
        }
    }

    public void ClickSystemButton()
    {
        GameStateManager.Instance.ClickedSystemButton = true;
        AudioManager.Instance.Play("sfx_consoleon", transform, 1.0f, false);
        if (SystemOrNav)
        {
            UIElements[0].transform.GetComponentInChildren<TextMeshProUGUI>().text = "Navigation";
            UIElements[1].GetComponent<TextMeshProUGUI>().text = "";
            SystemOrNav = false;
            for (int i = 2; i < UIElements.Count; i++)
            {
                UIElements[i].SetActive(true);
            }
        }
        else
        {
            UIElements[0].transform.GetComponentInChildren<TextMeshProUGUI>().text = "Systems";
            UIElements[1].GetComponent<TextMeshProUGUI>().text = "Console:\nProgress to Eden: " + (0 + 10 * (GameStateManager.Instance.dayCount % 10)) + " %\nTime to Destination: " + (10 - (1*(GameStateManager.Instance.dayCount%10)));;
            SystemOrNav = true;
            for (int i = 2; i < UIElements.Count; i++)
            {
                UIElements[i].SetActive(false);
            }
        }
        
    }

    public void ToggleVentilation()
    {
        if (isVentOn)
        {
            AudioManager.Instance.Play("sfx_switchoff", transform, 1.0f, false);
        }
        else
        {
            AudioManager.Instance.Play("sfx_switchon", transform, 1.0f, false);
        }
        isVentOn = !isVentOn;

    }

    public void ToggleThrusters()
    {
        //turn off thrusters briefly and then turn them back on 
        if (thrustersOn)
        {
            AudioManager.Instance.Play("sfx_switchoff", transform, 1.0f, false);
        }
        else
        {
            AudioManager.Instance.Play("sfx_switchon", transform, 1.0f, false);
        }
    }

    public void ToggleHeat()
    {
        if (thrustersOn)
        {
            AudioManager.Instance.Play("sfx_switchoff", transform, 1.0f, false);
        }
        else
        {
            AudioManager.Instance.Play("sfx_switchon", transform, 1.0f, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
