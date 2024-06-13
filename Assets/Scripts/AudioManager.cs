using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private GameObject[] soundEffects, voiceLines, ambientSounds;
    public List<AudioSource> currentSounds;

    // GameObject currentLoop = null; // have this line for scripts where you want audio to play
    // currentLoop = AudioManager.instance.LerpLoopable(loopName, transform, 2.0f); // copy paste this line when you want a loopable sound to play 
    // StartCoroutine(currentLoop.GetComponent<Loopable>().LerpDestroySelf(0.0f, 2.0f)); // copy paste this line when you want to kill a loopable sound
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
    
    public float Play(string name, Transform parent, float volume = 1.0f, bool playType = true)
    {
        GameObject audio;
        if(playType) // if its true look in the voicelines list else its a sound effect
        {
            audio = System.Array.Find(voiceLines, sound => sound.name == name);
        }
        else
        {
            audio = System.Array.Find(soundEffects, sound => sound.name == name);
        }
        
        if (audio == null)
        {
            Debug.Log("Sound: " + name + " not found");
            return 0;
        }
        AudioSource temp = Instantiate(audio, parent, false).GetComponent<AudioSource>();
        temp.volume = volume;
        return temp.clip.length;
    }

    public void deafenVolume(float volume = 0.5f)
    {
        foreach(AudioSource source in currentSounds)
        {
            source.volume = volume;
        }
    }

    public GameObject LerpLoopable(string name, Transform parent, float delay)
    {
        GameObject audio = System.Array.Find(soundEffects, sound => sound.name == name);

        if (audio == null)
        {
            Debug.Log("Loopable: " + name + " not found");
            return null;
        }

        Loopable loopable = Instantiate(audio, parent, false).GetComponent<Loopable>();
        StartCoroutine(loopable.Lerp(loopable.volume, delay));

        return loopable.gameObject;
    }
    /*
    public IEnumerator Play(string name, Transform parent, float delay)
    {
        GameObject audio = System.Array.Find(soundEffects, sound => sound.name == name);

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
