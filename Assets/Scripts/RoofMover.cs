using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofMover : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject roof1;
    private GameObject roof2;
    private Light light1;

    private GameObject floor;

    public GameObject Rig;

    void Start()
    {
        roof1 = this.transform.GetChild(0).gameObject;

        roof2 = this.transform.GetChild(1).gameObject;

        light1 = this.transform.GetChild(2).GetComponent<Light>();

        floor = this.transform.GetChild(3).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FaceReality()
    {
        StartCoroutine(OpenUpTheSkies());
    }

    public IEnumerator OpenUpTheSkies()
    {
        light1.intensity = 0;
        float timer = 0f;
        float timeToLive = 20f;
        Vector3 startingPositionR1 = roof1.transform.position;
        Vector3 startingPositionR2 = roof2.transform.position;
        Vector3 r1Target = new Vector3(roof1.transform.position.x, roof1.transform.position.y, -8f);
        Vector3 r2Target = new Vector3(roof2.transform.position.x, roof2.transform.position.y, 2.5f);
        yield return new WaitForSeconds(3);
        StartCoroutine(RaiseTheFloor());
        while (roof1.transform.position != r1Target)
        {
            float t = Mathf.SmoothStep(0, 1, timer / timeToLive);
            roof1.transform.position = Vector3.Lerp(startingPositionR1, r1Target, t);
            roof2.transform.position = Vector3.Lerp(startingPositionR2, r2Target, t);
            timer += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }

    public IEnumerator RaiseTheFloor()
    {
        float timer = 0f;
        float timeToLive = 40f;
        Vector3 startingPosition = floor.transform.position;
        Vector3 RigPos = Rig.transform.position;
        Vector3 floorTarget = new Vector3(floor.transform.position.x, 3.327f, floor.transform.position.z);
        while (floor.transform.position != floorTarget)
        {
            float t = Mathf.SmoothStep(0, 1, timer / timeToLive);
            floor.transform.position = Vector3.Lerp(startingPosition, floorTarget, t);
            Rig.transform.position = Vector3.Lerp(Rig.transform.position, new Vector3(Rig.transform.position.x, 3.327f, Rig.transform.position.z), t/120);
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
