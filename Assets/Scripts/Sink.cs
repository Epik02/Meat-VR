using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sink : MonoBehaviour
{
    public ParticleSystem waterParticle;

    private new Animation animation;
    private bool sinkWater;

    // Start is called before the first frame update
    void Start()
    {
        animation = GetComponent<Animation>();
        sinkWater = false;
        waterParticle.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        if (!sinkWater && Input.GetMouseButton(0) && !animation.isPlaying) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                animation.Play("SinkOn");
                waterParticle.Play();
                sinkWater = true;
            }
        }

        if (sinkWater && Input.GetMouseButton(0) && !animation.isPlaying)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                animation.Play("SinkOff");
                waterParticle.Stop();
                sinkWater = false;
            }
        }
    }
}
