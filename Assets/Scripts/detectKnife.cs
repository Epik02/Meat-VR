using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class detectKnife : MonoBehaviour
{
    public GameObject slicer;
    public Stopwatch timer = new Stopwatch();
    public float offsetx;
    public float offsety;
    public float roffsetx;
    public float roffsety;
    public float roffsetz;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "AccuracyTestKnife")
        {
            {
                timer.Start();
                slicer.transform.position = other.transform.position + new Vector3(offsetx, offsety, 0);

                Vector3 newRotation = new Vector3(other.transform.rotation.eulerAngles.x + roffsetx, other.transform.rotation.eulerAngles.y + roffsety, other.transform.rotation.eulerAngles.z + roffsetz);
                slicer.transform.rotation = Quaternion.Euler(newRotation);

                slicer.SetActive(true);

            }
        }    
    }

        // Update is called once per frame
        void Update()
    {
        if (timer.Elapsed.TotalSeconds > 1)
        {
            //slicer.transform.position = slicer.transform.position - new Vector3(0f, 0.2f, 0);
            slicer.SetActive(false);
            timer.Stop();
            timer.Reset();
        }
        else
        {
            slicer.transform.position = slicer.transform.position + new Vector3(0f, -0.01f, 0);
        }
    }
}
