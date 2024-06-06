using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OminiousVoice : MonoBehaviour
{
    // Start is called before the first frame update

    private void Awake()
    {
        GameStateManager.gameStateChanged += GameStateManagerGameStateChanged;
    }



    private void GameStateManagerGameStateChanged(GameStateManager.GameState state)
    {
        //turn off the helmet UI 
        if (state == GameStateManager.GameState.WakingUp)
        {
            Debug.Log("reached");
            int day = GameStateManager.Instance.dayCount;
            if (day == 1)
            {
                Debug.Log("Playing audio");
                float duration = AudioManager.Instance.Play("sfx_TempIntroVoiceline", transform);
                StartCoroutine(WaitForVoiceline(duration));
            }
            else if(day > 1 && day < 10)
            {
                StartCoroutine(WaitForVoiceline(3f));
            }
            else if(day >= 10)
            {
                StartCoroutine(WaitForVoiceline(10f));
            }
        }
        else
        {
            // move

        }
    }

    private IEnumerator WaitForVoiceline(float duration)
    {
        yield return new WaitForSeconds(duration);
        GameStateManager.Instance.ChangeGameState(GameStateManager.GameState.DayStart);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
