using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoapDispenser : MonoBehaviour
{
    public ParticleSystem soapParticle;

    private List<GameObject> hands = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        soapParticle.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        hands.Add(other.gameObject);
        soapParticle.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        hands.Remove(other.gameObject);
        if (hands.Count <= 0)
        {
            soapParticle.Stop();
        }
    }
}
