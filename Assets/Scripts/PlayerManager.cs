using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static PlayerManager Instance;
    private GameObject Movement;
    void Start()
    {
        Movement = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnOffMovement()
    {
        Movement.SetActive(false);
    }

    public void TurnOnMovement()
    {
        Movement.SetActive(true);
    }
}
