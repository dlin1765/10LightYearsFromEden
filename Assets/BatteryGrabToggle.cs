using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BatteryGrabToggle : MonoBehaviour
{
    // Start is called before the first frame update
    private XRGrabInteractable grabScript;
    private bool inSocket = false;
    void Start()
    {
        grabScript = GetComponent<XRGrabInteractable>();
    }

    

    public void TurnOffGrab()
    {
        grabScript.enabled = false;
    }

}
