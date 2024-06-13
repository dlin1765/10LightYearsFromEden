using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreakyTextSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject FreakyTxt, UICanvas;


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
            StartCoroutine(HelmetHallucination());
            AlarmManager.Instance.StartLightsOff(0, true);
        }
    }
    //Instantiate(Object original, Vector3 position, Quaternion rotation, Transform parent); 
    private IEnumerator HelmetHallucination()
    {
        int i = 0;
        while (i < 9)
        {
            yield return new WaitForSeconds(Random.Range(0.2f, 0.4f));
            Instantiate(FreakyTxt, UICanvas.transform);
            i += 1;
        }
        yield return new WaitForSeconds(1f);
        AlarmManager.Instance.StartLightsOff(0, false);
    }
}
