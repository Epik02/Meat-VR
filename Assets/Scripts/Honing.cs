using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Honing : MonoBehaviour
{
    public List<HoningCheckpoint> checkpoints;
    public ParticleSystem honingParticle;
    public bool check = false;
    public LayerMask knifeLayer;

    private Rigidbody rb;
    private GameObject knifeObject;
    private bool fullStraigthen = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        foreach (var honing in checkpoints)
        {
            // Check how many checkpoints are true
            if (honing.check)
            {
                i++;    // Add when a checkpoint is true
            }

            // If both checkpoints are true and the main honing is also true...
            if (i == checkpoints.Count && check)
            {
                // If the honing is fully clean...
                if (gameObject.GetComponent<Dirty>().dirtiness <= 0.0f && gameObject.GetComponent<Clean>().cleanness <= 0.0f && gameObject.GetComponent<Wet>().wetness <= 0.0f)
                {
                    // If the knife is on the correct angle and is not fully straighten...
                    if (knifeObject.GetComponent<KnifeStraighten>() && knifeObject.GetComponent<KnifeStraighten>().strength < 100.0f && knifeObject.GetComponent<KnifeStraighten>().correctAngle)
                    {
                        knifeObject.GetComponent<KnifeStraighten>().strength += 10.0f;                          // Straighten the knife by 10 (or whatever value you want)
                        AudioManager.instance.PlayOneShot(FMODEvents.instance.honing, this.transform.position); // Play honing sound once
                        fullStraigthen = false;                                                                 // Fully straighten is not true
                    }
                    // If the knife is fully straighten...
                    if (knifeObject.GetComponent<KnifeStraighten>().strength >= 100.0f && !fullStraigthen)
                    {
                        honingParticle.Play();                                                                  // Play complete particle
                        fullStraigthen = true;                                                                  // It is now full straighten
                    }
                }
                // Reset the process
                honing.check = false;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (rb == null)
        {
            return;
        }

        // If the knife layer matches the other objects layer...
        if (knifeLayer == (knifeLayer | (1 << other.gameObject.layer)))
        {
            if (other.collider.attachedRigidbody == null || other.collider.attachedRigidbody.mass > 0.0000001f)
            {
                knifeObject = other.gameObject; // The other object is the knife object
                check = true;                   // Is rubbing with the honing
            }
        }
    }

    private void OnCollisionExit(Collision other) 
    {
        if (rb == null)
        {
            return;
        }

        if (knifeLayer == (knifeLayer | (1 << other.gameObject.layer)))
        {
            if (other.collider.attachedRigidbody == null || other.collider.attachedRigidbody.mass > 0.0000001f)
            {
                // Reset everything when the knife is not rubbing anymore
                knifeObject = null;
                check = false;
                foreach (var honing in checkpoints)
                {
                    honing.check = false;
                }
            }
        }
    }
}
