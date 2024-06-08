using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenClose : MonoBehaviour
{
    // Start is called before the first frame update
    private float x, z, y;
    void Start()
    {
        x = transform.position.x;
        z = transform.position.z;
        y = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(StartOpenCloseDoor(4.5f));
        }
    }

    private IEnumerator StartOpenCloseDoor(float target)
    {
        float timer = 0;
        float duration = 1.5f;
        y = transform.position.y;

        while(timer < duration)
        {
            transform.position = new Vector3(x, Mathf.Lerp(y, target, timer / duration), z);
            yield return null;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(StartOpenCloseDoor(1.5f));
        }
    }
}
