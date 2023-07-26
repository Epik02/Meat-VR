using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clean : MonoBehaviour
{
    public float cleanness = 0.0f;

    private int mat;
    private MeshRenderer meshRenderer;
    private SkinnedMeshRenderer skinnedMeshRenderer;
    private bool skinned = false;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();

        if (skinnedMeshRenderer)
        {
            skinned = true;
        }

        if (skinned)
        {
            for (int i = 0; i < skinnedMeshRenderer.materials.Length; i++)
            {
                if (skinnedMeshRenderer.materials[i].name.Contains("soap"))
                {
                    mat = i;
                }
            }
        }
        else
        {
            for (int i = 0; i < meshRenderer.materials.Length; i++)
            {
                if (meshRenderer.materials[i].name.Contains("soap"))
                {
                    mat = i;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (skinned)
        {
            Color c = skinnedMeshRenderer.materials[mat].color;
            c.a = cleanness / 100.0f;
            skinnedMeshRenderer.materials[mat].color = c;
        }
        else
        {
            Color c = meshRenderer.materials[mat].color;
            c.a = cleanness / 100.0f;
            meshRenderer.materials[mat].color = c;
        }
    }
}