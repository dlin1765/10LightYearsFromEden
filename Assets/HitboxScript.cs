using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxScript : MonoBehaviour
{
    // Start is called before the first frame update
    public static HitboxScript Instance;
    public bool playerInUnsafeZone = false;
    public GameObject Console;
    private ConsoleScript ConsoleUI;
    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Start()
    {
        ConsoleUI = Console.GetComponent<ConsoleScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInUnsafeZone = true;
            Debug.Log("player touched collider");
            if (HelmetUIManager.Instance.helmetOn)
            {
                Debug.Log("has helmet on proceed");
            }
            else
            {
                AlarmManager.Instance.StartAlarm(ConsoleUI.isVentOn);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            playerInUnsafeZone = false;
            AlarmManager.Instance.StopAlarm();
        }
    }

    

}
