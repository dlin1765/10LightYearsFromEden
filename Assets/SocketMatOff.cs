using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketMatOff : MonoBehaviour
{
    // Start is called before the first frame update
    private List<Material> TransGreen = new List<Material>();
    private List<Material> emptyList = new List<Material>();
    private MeshRenderer meshRenderer;
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        TransGreen.Add(meshRenderer.material);
    }

    public void TurnOffMat()
    {
        meshRenderer.SetMaterials(emptyList);
        AudioManager.Instance.Play("sfx_spark", transform, 1.0f, false);
    }

    public void TurnOnMat()
    {
        meshRenderer.SetMaterials(TransGreen);
    }
}
