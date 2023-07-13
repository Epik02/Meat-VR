using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class detectKnife : MonoBehaviour
{
    public GameObject slicer;
    public Stopwatch timer = new Stopwatch();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "AccuracyTestKnife")
        {
            {
                timer.Start();
                slicer.transform.position = other.transform.position + new Vector3(-0.5f,-0.07f,0);
                slicer.SetActive(true);

            }
        }    
    }

        // Update is called once per frame
        void Update()
    {
        if (timer.Elapsed.TotalSeconds > 1)
        {
            slicer.SetActive(false);
            timer.Stop();
            timer.Reset();
        }
    }
}
