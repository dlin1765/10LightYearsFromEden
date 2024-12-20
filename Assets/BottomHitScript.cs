using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomHitScript : MonoBehaviour
{
    // Start is called before the first frame update
    private FuelCellScript parentObject;
    void Start()
    {
        parentObject = transform.parent.gameObject.GetComponent<FuelCellScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Stopper"))
        {
            parentObject.BottomHit();
        }
    }
  
}
