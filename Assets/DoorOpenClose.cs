using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenClose : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StartOpen()
    {
        StartCoroutine(StartOpenCloseDoor(4.5f));
    }

    public void StartClose()
    {
        StartCoroutine(StartOpenCloseDoor(1.5f));
    }

    private IEnumerator StartOpenCloseDoor(float target)
    {
        Debug.Log("should be going up");
        float timer = 0;
        float duration = 1.5f;
        Vector3 startPos = transform.localPosition;
        Vector3 targetPos = new Vector3(transform.localPosition.x, target, transform.localPosition.z);

        while(timer < duration)
        {
            transform.localPosition = Vector3.Lerp(startPos, targetPos, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = targetPos;
    }

    
}
