using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soap : MonoBehaviour
{
    public float soapPerParticle;
    public ParticleSystem ps;

    private List<Clean> soapObjects;

    // Start is called before the first frame update
    void Start()
    {
        soapObjects = new List<Clean>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnParticleCollision(GameObject other)
    {
        Clean clean = other.GetComponent<Clean>();                  // Must check if the object has a Clean component
        Clean cleanChild = other.GetComponentInChildren<Clean>();   // If they do not have a Clean component then check if the objects children have a Clean component

        Dirty dirty = other.GetComponent<Dirty>();                  // Must check if the object has a Dirty component
        Dirty dirtyChild = other.GetComponentInChildren<Dirty>();   // If they do not have a Dirty component then check if the objects children have a Dirty component

        if (clean)                                                  // Checks if the player does have a Clean component and if true...
        {
            if (!soapObjects.Contains(clean))                       // If clean is not on the soaped list then we add it
            {
                soapObjects.Add(clean);
            }
            if (clean.cleanness < 100.0f)                           // If cleanness is not fully...
            {
                soapObjects.Remove(clean);                          // Remove from the soaped list
                clean.cleanness += soapPerParticle;                 // Add to cleanness per particle collision
            }
            if (dirty.dirtiness > 0.0f)                             // If dirtiness is not fully gone...
            {
                soapObjects.Remove(clean);                          // Remove from soaped list
                dirty.dirtiness -= soapPerParticle;                 // Remove to dirtiness per particle collision
            }
            if (!soapObjects.Contains(clean) && clean.cleanness >= 100.0f && dirty.dirtiness <= 0.0f)   // If fully cleaned and fully undirty...
            {
                soapObjects.Add(clean);                             // Add back to soaped list
                ps.Play();                                          // Play complete particle
            }
        }

        if (cleanChild)                                             // Same as above but with the children components
        {
            if (!soapObjects.Contains(cleanChild))
            {
                soapObjects.Add(clean);
            }
            if (cleanChild.cleanness < 100.0f)
            {
                soapObjects.Remove(clean);
                cleanChild.cleanness += soapPerParticle;
            }
            if (dirtyChild.dirtiness > 0.0f)
            {
                soapObjects.Remove(cleanChild);
                dirtyChild.dirtiness -= soapPerParticle;
            }
            if (!soapObjects.Contains(cleanChild) && cleanChild.cleanness >= 100.0f && dirtyChild.dirtiness <= 0.0f)
            {
                soapObjects.Add(cleanChild);
                ps.Play();
            }
        }
    }
}
