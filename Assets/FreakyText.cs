using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FreakyText : MonoBehaviour
{
    // Start is called before the first frame update
    private TextMeshProUGUI myText;
    //, 
    void Start()
    {
        myText = GetComponent<TextMeshProUGUI>();
        int i = Random.Range(0, 5);
        switch (i)
        {
            case 0:
                myText.text = "keep me on";
                break;
            case 1:
                myText.text = "nothings wrong";
                break;
            case 2:
                myText.text = "wake up";
                break;
            case 3:
                myText.text = "make it stop";
                break;
            case 4:
                myText.text = "ignore it";
                break;
            case 5:
                myText.text = "they're lying";
                break;
        }
        transform.localPosition = new Vector3(Random.Range(-0.8f, 0.8f), Random.Range(-0.8f, 0.5f), 1f);
        transform.Rotate(Vector3.forward * Random.Range(-45f, 45f));
        StartCoroutine(StartDestroy());
    }

    private IEnumerator StartDestroy()
    {
        yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));
        Destroy(this.gameObject);
    }
}
