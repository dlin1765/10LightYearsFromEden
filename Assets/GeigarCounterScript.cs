using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GeigarCounterScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject RadTextObj;
    private TextMeshProUGUI RadCounterText;
    private GameObject counterCanvas;
    GameObject currentLoop = null;

    private Coroutine Flicker = null;
    void Start()
    {
        counterCanvas = transform.GetChild(1).gameObject;
        RadCounterText = RadTextObj.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnOnScreen()
    {
        GameStateManager.Instance.GeigarCounterDiscovered = true;
        currentLoop = AudioManager.Instance.LerpLoopable("sfx_geigarcounter", transform, 0.0f);
        counterCanvas.SetActive(true);
        Flicker = StartCoroutine(FlickerRadVals());
    }

    private IEnumerator FlickerRadVals()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 1f));
            RadCounterText.text = Random.Range(0.00f, 3.00f).ToString();
        }
    }

    public void TurnOffScreen()
    {
        if(Flicker != null)
        {
            StopCoroutine(Flicker);
        }
        StartCoroutine(currentLoop.GetComponent<Loopable>().LerpDestroySelf(0.0f, 0.1f));
        counterCanvas.SetActive(false);
    }

    
}
