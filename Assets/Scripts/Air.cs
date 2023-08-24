using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Air : MonoBehaviour
{
    public float dryPerParticle = 0.5f;
    public ParticleSystem ps;

    private List<Wet> dryObjects;

    // Start is called before the first frame update
    void Start()
    {
        dryObjects = new List<Wet>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        Wet wet = other.GetComponent<Wet>();                    // Must check if the object has a Wet component
        Wet wetChild = other.GetComponentInChildren<Wet>();     // If they do not have a Wet component then check if the objects children have a Wet component

        if (other.CompareTag("Player"))                         // This is specifically for the hands
        {
            if (wet)                                                    // Checks if the player does have a Wet component and if true...
            {
                if (!dryObjects.Contains(wet))                          // If wet is not on the dry list then we add it 
                {
                    dryObjects.Add(wet);
                }
                if (wet.wetness > 0.0f)                                 // If wetness is not dried off yet then...
                {
                    dryObjects.Remove(wet);                             // It will be removed from the dry list
                    wet.wetness -= dryPerParticle;                      // Wetness will be decreased per particle colliding
                }
                if (!dryObjects.Contains(wet) && wet.wetness <= 0.0f)   // If wet object is fully dry and not in dry list then...
                {
                    dryObjects.Add(wet);                                // Add to dry list
                    ps.Play();                                          // Play complete particle effect
                }
            }

            if (wetChild)                                               // Checks if the player does have a Wet component in children and if true...
            {                                                           // Same outcomes as above but with Wet component in children
                if (!dryObjects.Contains(wetChild))
                {
                    dryObjects.Add(wetChild);
                }
                if (wetChild.wetness > 0.0f)
                {
                    dryObjects.Remove(wetChild);
                    wetChild.wetness -= dryPerParticle;
                }
                if (!dryObjects.Contains(wetChild) && wetChild.wetness <= 0.0f)
                {
                    dryObjects.Add(wetChild);
                    ps.Play();
                }
            }
        }
    }
}
