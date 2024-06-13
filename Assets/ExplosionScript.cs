using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{

    // Start is called before the first frame update
    [SerializeField] private GameObject ExplosionSource;
    [SerializeField] private GameObject rig;
    [SerializeField] private float explosionForce, playerExplosionForce, radius;
    [SerializeField] private List<Rigidbody> shipPartsList;

    public GameObject otherShip;

    private Rigidbody playerRig;
    void Start()
    {
        otherShip.SetActive(false);
        for(int i = 0; i < transform.childCount; i++)
        {
            shipPartsList.Add(transform.GetChild(i).GetComponent<Rigidbody>());
        }
        playerRig = rig.GetComponent<Rigidbody>();
        StartCoroutine(StartExplosion());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator StartExplosion()
    {
        AudioManager.Instance.Play("sfx_explosion", transform, 1.0f, false);
        foreach (Rigidbody rb in shipPartsList)
        {
            rb.AddExplosionForce(explosionForce, ExplosionSource.transform.position, radius); 
        }
        playerRig.AddExplosionForce(playerExplosionForce, ExplosionSource.transform.position, radius);
        yield return null;
    }
}
