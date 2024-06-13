using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlash : MonoBehaviour
{
    // Start is called before the first frame update
    private Light myLight;

    private float Intensity = 21.26f;
    void Start()
    {
        myLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AlarmMode()
    {
        StartCoroutine(Flashing());
    }

    public void LightOffOrOn(bool off = true)
    {
        if(off)
        {
            myLight.intensity = 0f;
        }
        else
        {
            myLight.intensity = Intensity;
        }
    }

    public void LightOn()
    {
        myLight.intensity = Intensity;
    }

    private IEnumerator Flicker(float duration)
    {
        myLight.intensity = 0f;
        yield return new WaitForSeconds(duration);
        myLight.intensity = Intensity;
    }

    private IEnumerator Flashing()
    {
        myLight.color = new Color32(255, 0, 0, 255);
        myLight.intensity = 21.26f;
        float counter = 0;
        float duration = 0.75f;

        while (true)
        {
            counter = 0;
            while (counter < duration)
            {
                counter += Time.deltaTime;
                myLight.intensity = Mathf.Lerp(Intensity, 0, counter / duration);
                yield return null;
            }
            counter = 0;
            yield return new WaitForSeconds(0.1f);
            while (counter < duration)
            {
                counter += Time.deltaTime;
                myLight.intensity = Mathf.Lerp(0, Intensity, counter / duration);
                yield return null;
            }
        }
    }

    public void StopAlarmMode()
    {
        myLight.color = new Color32(251, 255, 235, 255);
        myLight.intensity = 21.26f;
        StopAllCoroutines();
    }
}
