using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static FadeManager Instance;
    private Image BlackImage;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        BlackImage = transform.GetChild(0).GetChild(0).gameObject.GetComponent<Image>();
    }

    public void StartFadeToBlack(float duration, float delay = 2f)
    {
        StartCoroutine(fToBlack(duration, delay));
    }

    private IEnumerator fToBlack(float duration, float delay)
    {
        float changeAmount = 1.0f / (duration * 60);
        yield return new WaitForSeconds(delay);
        float timer = 0f;
        while (duration >= timer)
        {
            timer += Time.deltaTime;
            BlackImage.color = new Color(0f, 0f, 0f, BlackImage.color.a + changeAmount);
            yield return null;
        }
    }

    public void StartFadeFromBlack(float duration, float delay = 2f)
    {
        StartCoroutine(FfromBlack(duration, delay));
    }

    private IEnumerator FfromBlack(float duration, float delay = 2f)
    {
        float changeAmount = 1.0f / (duration * 60);
        yield return new WaitForSeconds(delay);
        float timer = 0f;
        while(duration >= timer)
        {
            timer += Time.deltaTime;
            BlackImage.color = new Color(0f, 0f, 0f, BlackImage.color.a - changeAmount);
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
