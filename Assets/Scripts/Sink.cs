using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sink : MonoBehaviour
{
    public ParticleSystem waterParticle;
    public float minWater = 40.0f;
    public float halfWater = 90.0f;
    public float maxWater = 130.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(transform.rotation.eulerAngles);

        if (transform.rotation.eulerAngles.z <= minWater) 
        {
            var emission = waterParticle.emission;
            emission.rateOverTime = 0.0f;
        }
        if (transform.rotation.eulerAngles.z >= halfWater)
        {
            var emission = waterParticle.emission;
            emission.rateOverTime = transform.rotation.eulerAngles.z * 0.25f;
        }
        if (transform.rotation.eulerAngles.z >= maxWater)
        {
            var emission = waterParticle.emission;
            emission.rateOverTime = transform.rotation.eulerAngles.z * 5;
        }
    }
}
