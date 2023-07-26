using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandDryer : MonoBehaviour
{
    public ParticleSystem airParticle;

    private List<GameObject> hands = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        airParticle.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        hands.Add(other.gameObject);
        airParticle.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        hands.Remove(other.gameObject);
        if (hands.Count <= 0)
        {
            airParticle.Stop();
        }
    }
}
