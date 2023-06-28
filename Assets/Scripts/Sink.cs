using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sink : MonoBehaviour
{
    public ParticleSystem waterParticle;

    private List<GameObject> hands = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        waterParticle.Stop();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        hands.Add(other.gameObject);
        waterParticle.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        hands.Remove(other.gameObject);
        if (hands.Count <= 0)
        {
            waterParticle.Stop();
        }
    }
}
