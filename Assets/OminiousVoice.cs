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
                float duration = AudioManager.Instance.Play("sfx_Voiceline1", transform, 1f, true);
                StartCoroutine(WaitForVoiceline(duration));
            }
            else if(day == 2)
            {
                float duration = AudioManager.Instance.Play("sfx_FriendlyReminder", transform, 1f, true);
                StartCoroutine(WaitForVoiceline(duration));
            }
            else if(day == 5)
            {
                float duration = AudioManager.Instance.Play("sfx_Turbulence", transform, 1f, true);
                StartCoroutine(WaitForVoiceline(duration));
            }
            else if(day == 8)
            {
                float duration = AudioManager.Instance.Play("sfx_WearAndTear", transform, 1f, true);
                StartCoroutine(WaitForVoiceline(duration));
            }
            else
            {
                float duration = AudioManager.Instance.Play("sfx_VoicelineGeneral", transform, 1f, true);
                StartCoroutine(WaitForVoiceline(duration));
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
