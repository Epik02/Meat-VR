using Autohand;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DirtyGloves : MonoBehaviour
{
    public float dirtiness = 0;

    private int mat;
    private SkinnedMeshRenderer skinnedMeshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();

        for (int i = 0; i < skinnedMeshRenderer.materials.Length; i++)  // Take each skinned mesh renderer material
        {
            if (skinnedMeshRenderer.materials[i].name.Contains("Dirt")) // If material has dirt in it then...
            {
                mat = i;                                                // That is the material we will change transparency
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Color c = skinnedMeshRenderer.materials[mat].color;     // Creates a new color from the dirt material
        c.a = dirtiness / 100.0f;                               // Changes the alpha of the color depending on the dirtiness value
        skinnedMeshRenderer.materials[mat].color = c;           // Puts the new color back into the original materials spot

        if (dirtiness >= 100.0f)                                // If the gloves are dirty do not pick up objects
        {
            var grabbables = FindObjectsOfType(typeof(Grabbable));
            foreach (var item in grabbables)
            {
                item.GetComponent<Grabbable>().enabled = false;
            }
        }
        else                                                    // When gloves are clean you can pick up objects
        {
            var grabbables = FindObjectsOfType(typeof(Grabbable));
            foreach (var item in grabbables)
            {
                item.GetComponent<Grabbable>().enabled = true;
            }
        }
    }
}
