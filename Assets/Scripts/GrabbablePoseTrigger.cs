using Autohand;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbablePoseTrigger : MonoBehaviour
{
    public GameObject soapParticle;
    public Transform particleStart;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // When spray bottle is triggered it will create a sanitize particle
    public void UseBottle()
    {
        GameObject newParticle = Instantiate(soapParticle);                                             // Create a new santize particle
        newParticle.transform.position = particleStart.position;                                        // Particle position starts at the start position
        newParticle.transform.rotation = gameObject.transform.rotation;
        newParticle.transform.localScale = Vector3.one;
        AudioManager.instance.PlayOneShot(FMODEvents.instance.sprayBottle, this.transform.position);    // Play spray bottle sound once
        if (newParticle.GetComponent<ParticleSystem>().isStopped)                                       // When the particle has stopped playing...
        {
            Destroy(newParticle);                                                                       // Delete the particle object
        }
    }
}
