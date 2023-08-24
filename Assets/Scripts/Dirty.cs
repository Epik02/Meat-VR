using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dirty : MonoBehaviour
{
    public float dirtiness = 0.0f;

    private int mat;
    private MeshRenderer meshRenderer;
    private SkinnedMeshRenderer skinnedMeshRenderer;
    private bool skinned = false;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();

        if (skinnedMeshRenderer)                                            // Checks if object has skinned mesh renderer or not
        {
            skinned = true;
        }

        if (skinned)                                                        // If object does have skinned mesh renderer...
        {
            for (int i = 0; i < skinnedMeshRenderer.materials.Length; i++)  // Take each skinned mesh renderer material
            {
                if (skinnedMeshRenderer.materials[i].name.Contains("Dirt")) // If material has dirt in it then...
                {
                    mat = i;                                                // That is the material we will change transparency
                }
            }
        }
        else                                                                // If object has a mesh renderer...
        {                                                                   // Same thing as above
            for (int i = 0; i < meshRenderer.materials.Length; i++)
            {
                if (meshRenderer.materials[i].name.Contains("Dirt"))
                {
                    mat = i;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(skinned)
        {
            Color c = skinnedMeshRenderer.materials[mat].color;     // Creates a new color from the dirt material
            c.a = dirtiness / 100.0f;                               // Changes the alpha of the color depending on the dirtiness value
            skinnedMeshRenderer.materials[mat].color = c;           // Puts the new color back into the original materials spot
        }
        else
        {
            Color c = meshRenderer.materials[mat].color;            // Same proccess
            c.a = dirtiness / 100.0f;
            meshRenderer.materials[mat].color = c;
        }
    }
}
