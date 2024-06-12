using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetPositionReset : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject ResetPosition;
    void Start()
    {
        
    }
    public void ResetPos()
    {
        transform.localPosition = ResetPosition.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
