using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dirty : MonoBehaviour
{
    public float dirtiness = 100.0f;

    private MeshRenderer meshRenderer;
    private Material material;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Color c = meshRenderer.material.color;
        c.a = dirtiness / 100.0f;
        meshRenderer.material.color= c;
    }
}
