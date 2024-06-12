using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleCollisionDetection : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject ResetPosition;
    private FuelCellScript parentObject;
    private Vector3 localPos;
    void Start()
    {
        parentObject = transform.parent.parent.gameObject.GetComponent<FuelCellScript>();
        localPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetLocalPos()
    {
        localPos = transform.localPosition;
    }
    public void ResetPos()
    {
        transform.localPosition = ResetPosition.transform.localPosition;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Stopper")
        {
            parentObject.TopHit();
        }
    }
}
