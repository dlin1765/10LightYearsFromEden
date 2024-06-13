using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreakyTextSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject FreakyTxt, UICanvas;

    GameObject currentLoop = null;
    private List<string> Options = new List<string>();
    void Start()
    {
        Options.Add("keep me on");
        Options.Add("nothings wrong");
        Options.Add("wake up");
        Options.Add("make it stop");
        Options.Add("ignore it");
    }

    public void StartSpawningText()
    {
        if (HelmetUIManager.Instance.PoisonDiscovered)
        {
            GameStateManager.Instance.SeenHallucination = true;
            StartCoroutine(HelmetHallucination());
            AlarmManager.Instance.StartLightsOff(0, true);
            GameStateManager.Instance.TurnOffShipSound();
        }
    }
    //Instantiate(Object original, Vector3 position, Quaternion rotation, Transform parent); 
    private IEnumerator HelmetHallucination()
    {
        AudioManager.Instance.Play("sfx_powerdown", PlayerManager.Instance.transform, 1.0f, false);
        yield return new WaitForSeconds(2f);
        currentLoop = AudioManager.Instance.LerpLoopable("sfx_fnafsound", transform, 0.0f);
        int i = 0;
        while (i < 9)
        {
            yield return new WaitForSeconds(Random.Range(0.2f, 0.4f));
            Instantiate(FreakyTxt, UICanvas.transform);
            i += 1;
        }
        StartCoroutine(currentLoop.GetComponent<Loopable>().LerpDestroySelf(0.0f, 0.1f));
        yield return new WaitForSeconds(1f);
        AlarmManager.Instance.StartLightsOff(0, false);
        AudioManager.Instance.Play("sfx_powerup", PlayerManager.Instance.transform, 1.0f, false);
        GameStateManager.Instance.TurnOnShipSound();
        AudioManager.Instance.Play("sfx_lightsflickeron", PlayerManager.Instance.transform, 1.0f, false);

    }
}
