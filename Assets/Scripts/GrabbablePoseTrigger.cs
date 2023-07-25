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

    public void UseBottle()
    {
        GameObject newParticle = Instantiate(soapParticle);
        //newParticle.transform.parent = gameObject.transform;
        newParticle.transform.position = particleStart.position;
        newParticle.transform.rotation = gameObject.transform.rotation;
        newParticle.transform.localScale = Vector3.one;
        if (newParticle.GetComponent<ParticleSystem>().isStopped)
        {
            Destroy(newParticle);
        }
        //soapParticle.Play();
    }
}
