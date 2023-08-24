using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanWater : MonoBehaviour
{
    public ParticleSystem finishedParticle;
    public float cleanPerParticle = 0.1f;

    private List<Wet> wetObjects;

    // Start is called before the first frame update
    void Start()
    {
        wetObjects = new List<Wet>();
        finishedParticle.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        Dirty dirty = other.GetComponent<Dirty>();                      // Must check if the object has a Dirty component
        Dirty dirtyChild = other.GetComponentInChildren<Dirty>();       // If they do not have a Dirty component then check if the objects children have a Dirty component

        Clean clean = other.GetComponent<Clean>();                      // Must check if the object has a Clean component
        Clean cleanChild = other.GetComponentInChildren<Clean>();       // If they do not have a Clean component then check if the objects children have a Clean component

        Wet wet = other.GetComponent<Wet>();                            // Must check if the object has a Wet component
        Wet wetChild = other.GetComponentInChildren<Wet>();             // If they do not have a Wet component then check if the objects children have a Wet component

        if (clean && wet)                                                                       // Checks if the player does have a Clean and Wet component and if true...
        {
            if (!wetObjects.Contains(wet))                                                      // If wet is not on the wet list then we add it 
            {
                wetObjects.Add(wet);
            }
            if (clean.cleanness > 0.0f && dirty.dirtiness <= 0.0f)                              // If soap is fully on then...
            {
                wetObjects.Remove(wet);                                                         // Remove wet from wet list
                clean.cleanness -= cleanPerParticle;                                            // Decrease cleanness by particle collision
            }
            if (wet.wetness < 100.0f && dirty.dirtiness <= 0.0f)                                // If not fully wet then...
            {
                wetObjects.Remove(wet);                                                         // Remove from wet list
                wet.wetness += cleanPerParticle;                                                // Increase wetness by particle collision
            }
            if (!wetObjects.Contains(wet) && clean.cleanness <= 0.0f && wet.wetness >= 100.0f)  // If soap has been removed and is now fully wet...
            {
                wetObjects.Add(wet);                                                            // Add object to wet list
                finishedParticle.Play();                                                        // Play finished particle
            }
        }

        if (cleanChild && wetChild)                                                             // Same thing as above but for components in children
        {
            if (!wetObjects.Contains(wetChild))
            {
                wetObjects.Add(wetChild);
            }
            if (cleanChild.cleanness > 0.0f && dirtyChild.dirtiness <= 0.0f)
            {
                wetObjects.Remove(wetChild);
                cleanChild.cleanness -= cleanPerParticle;
            }
            if (wetChild.wetness < 100.0f && dirtyChild.dirtiness <= 0.0f)
            {
                wetObjects.Remove(wetChild);
                wetChild.wetness += cleanPerParticle;
            }
            if (!wetObjects.Contains(wetChild) && cleanChild.cleanness <= 0.0f && wetChild.wetness >= 100.0f)
            {
                wetObjects.Add(wetChild);
                finishedParticle.Play();
            }
        }
    }
}
