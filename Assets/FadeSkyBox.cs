using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeSkyBox : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private List<SpriteRenderer> SkyBoxList;
    void Start()
    { 
        for(int i = 0; i < transform.childCount; i++)
        {
            SkyBoxList.Add(transform.GetChild(i).GetComponent<SpriteRenderer>());
        }
    }

    public void StartFadeOutSkybox()
    {
        StartCoroutine(FadeOutSkyBox());
    }
    private IEnumerator FadeOutSkyBox()
    {
        float timer = 0;
        float duration = 5f;
        while(timer < duration)
        {
            foreach(SpriteRenderer sr in SkyBoxList)
            {
                timer += Time.deltaTime;
                sr.color = new Color(255f, 255f, 255f, Mathf.Lerp(1f, 0f, timer / duration));
                yield return null;
            }
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
