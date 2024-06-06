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
        yield return new WaitForSeconds(delay);
        float timer = 0f;
        while (timer<duration)
        {
            timer += Time.deltaTime;
            BlackImage.color = new Color(0f, 0f, 0f, Mathf.Lerp(0f, 1f, timer / duration));
            yield return null;
        }
        if(GameStateManager.Instance.currentGameState == GameStateManager.GameState.SleepTime)
        {
            yield return new WaitForSeconds(3f);
            GameStateManager.Instance.ChangeGameState(GameStateManager.GameState.WakingUp);
        }
    }

    public void StartFadeFromBlack(float duration, float delay = 2f)
    {
        StartCoroutine(FfromBlack(duration, delay));
    }

    private IEnumerator FfromBlack(float duration, float delay = 2f)
    {
        yield return new WaitForSeconds(delay);
        float timer = 0f;
        while(timer < duration)
        {
            timer += Time.deltaTime;
            BlackImage.color = new Color(0f, 0f, 0f, Mathf.Lerp(1f, 0f, timer/duration));
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
