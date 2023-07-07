using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanWater : MonoBehaviour
{
    public LayerMask whatToClean;
    public float cleanPerParticle = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        Dirty dirty = other.GetComponent<Dirty>();
        Dirty dirtyChild = other.GetComponentInChildren<Dirty>();

        if (dirty)
        {
            if (dirty.dirtiness > 0.0f)
            {
                dirty.dirtiness -= cleanPerParticle;
            }
        }

        if (dirtyChild)
        {
            if (dirtyChild.dirtiness > 0.0f)
            {
                dirtyChild.dirtiness -= cleanPerParticle;
            }
        }
    }
}
