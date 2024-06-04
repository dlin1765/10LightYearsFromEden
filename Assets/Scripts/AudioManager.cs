using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private GameObject[] soundEffects, voiceLines, ambientSounds;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
    public float Play(string name, Transform parent)
    {
        GameObject audio = System.Array.Find(sounds, sound => sound.name == name);

        if (audio == null)
        {
            Debug.Log("Sound: " + name + " not found");
            return 0;
        }

        return Instantiate(audio, parent, false).GetComponent<AudioSource>().clip.length;
    }

    public IEnumerator Play(string name, Transform parent, float delay)
    {
        GameObject audio = System.Array.Find(sounds, sound => sound.name == name);

        if (audio == null)
        {
            Debug.Log("Sound: " + name + " not found");
            yield break;
        }

        yield return new WaitForSeconds(delay);
        GameObject prefab = Instantiate(audio, parent, false);
    }
    */
}
