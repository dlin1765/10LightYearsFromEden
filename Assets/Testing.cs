using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ShipPrefab;
    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Instantiate(ShipPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            ShipPrefab.SetActive(true);
        }
    }
}
