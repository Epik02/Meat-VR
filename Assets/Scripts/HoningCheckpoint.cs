using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoningCheckpoint : MonoBehaviour
{
    public bool check = false;

    //private void OnCollisionEnter(Collision other)
    //{
    //    if (other.gameObject.CompareTag("Knife"))
    //    {
    //        check = true;
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Knife"))
        {
            check = true;
        }
    }
}
