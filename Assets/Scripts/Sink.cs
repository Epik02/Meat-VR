using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sink : MonoBehaviour
{
    public ParticleSystem waterParticle;

    private bool isTriggered;

    // Start is called before the first frame update
    void Start()
    {
        waterParticle.Stop();
        isTriggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTriggered)
        {
            waterParticle.Play();
        }
        else
        {
            waterParticle.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        isTriggered = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isTriggered = false;
    }
}
