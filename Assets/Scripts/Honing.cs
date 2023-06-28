using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Honing : MonoBehaviour
{
    public List<HoningCheckpoint> checkpoints;
    public bool check = false;

    private GameObject knifeObject;

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        foreach (var honing in checkpoints)
        {
            if (honing.check)
            {
                i++;
            }

            if (i == checkpoints.Count && check)
            {
                if (knifeObject.GetComponent<SliceListener>())
                {
                    knifeObject.GetComponent<SliceListener>().slicer.strength += 10.0f;
                }

                honing.check = false;
            }
        }
    }

    //private void OnCollisionEnter(Collision other)
    //{
    //    if (other.gameObject.CompareTag("Knife"))
    //    {
    //        knifeObject = other.gameObject;
    //        check = true;
    //    }
    //}

    //private void OnCollisionExit(Collision other)
    //{
    //    knifeObject = null;
    //    check = false;
    //    foreach (var honing in checkpoints) 
    //    {
    //        honing.check = false;
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Knife"))
        {
            knifeObject = other.gameObject;
            check = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        knifeObject = null;
        check = false;
        foreach (var honing in checkpoints)
        {
            honing.check = false;
        }
    }
}
