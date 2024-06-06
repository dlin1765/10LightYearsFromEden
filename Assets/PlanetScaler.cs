using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetScaler : MonoBehaviour
{
    public static PlanetScaler Instance;
    private SpriteRenderer image;
    private void Awake()
    {
        Instance = this;
        GameStateManager.gameStateChanged += GameStateManagerGameStateChanged;
    }
    // day 1 = 0 day 1 = 1 day 3 = 2 
    
    private void GameStateManagerGameStateChanged(GameStateManager.GameState state)
    {
        //turn off the helmet UI 
        if (state == GameStateManager.GameState.WakingUp)
        {
            int day = GameStateManager.Instance.dayCount;
            if (day % 10 == 0)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                transform.localScale = new Vector3(1f + 0.5f * (day%10), 1f + 0.5f * (day%10), 1f + 0.5f * (day%10));
            }
        }
        else if(state == GameStateManager.GameState.FinalCutscene)
        {
            transform.localScale = new Vector3(6f, 6f, 6f);
            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<SpriteRenderer>();
    }

    public void StartPlanetFade()
    {
        StartCoroutine(PlanetFadeAway());
    }
    private IEnumerator PlanetFadeAway()
    {
        Debug.Log("planet should fade");
        float timer = 0;
        float duration = 8f;
        while(timer < duration)
        {
            timer += Time.deltaTime;
            image.color = new Color(255f, 255f, 255f, Mathf.Lerp(1f, 0f, timer / duration));
            yield return null;
        }

    }

    // Update is called once per frame
    void Update()
    {
    }
}
