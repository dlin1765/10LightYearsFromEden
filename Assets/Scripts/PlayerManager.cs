using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static PlayerManager Instance;
    private GameObject Movement;
    private Vector3 FinalPos = new Vector3(0, 0, 50f);

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Movement = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnOffMovement()
    {
        Movement.SetActive(false);
    }

    public void TurnOnMovement()
    {
        Movement.SetActive(true);
    }

    public void StartFloating()
    {
        this.gameObject.AddComponent<Rigidbody>().useGravity = false;
        StartCoroutine(SitDown());
    }
    private IEnumerator SitDown()
    {
        float timer = 0f;
        float duration = 3f;
        while(timer < duration)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, transform.position.y - 0.5f, timer / duration), transform.position.z);
            yield return null;
        }
        Movement.transform.GetChild(1).GetComponent<UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets.DynamicMoveProvider>().useGravity = false;
    }
    
    public void SetPlayerFinal()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        transform.position = FinalPos;
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finish")
        {
            Debug.Log("door hitbox detected");
            other.GetComponent<DoorOpenClose>().StartOpen();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Finish")
        {
            other.GetComponent<DoorOpenClose>().StartClose();
        }
    }
}
