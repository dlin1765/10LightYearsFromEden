using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelCellScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] bool lockedPosition = false;
    [SerializeField] int BatteryReq = 2, NumBatteries = 0;
    private GameObject topStopper;
    private GameObject botStopper;
    private Vector3 lockedPos;
    private Vector3 defaultPos;
    void Start()
    {
        topStopper = transform.GetChild(1).gameObject;
        botStopper = transform.GetChild(2).gameObject;
        lockedPos = transform.GetChild(3).localPosition;
        defaultPos = transform.GetChild(4).localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Grabbed()
    {
        if (lockedPosition)
        {
            lockedPosition = false;
            botStopper.transform.localPosition = defaultPos; 
        }
    }

    public void TopHit()
    {
        lockedPosition = true;
        botStopper.transform.localPosition = lockedPos;
    }

    public void BottomHit()
    {
        if(NumBatteries >= BatteryReq)
        {
            // fuel task done here
            Debug.Log("Fuel task done");
            HelmetUIManager.Instance.CompleteFuel();
        }
    }

    public void BatteryEntered()
    {
        NumBatteries += 1;
    }

    public void BatteryExited()
    {
        NumBatteries -= 1;
    }
}
