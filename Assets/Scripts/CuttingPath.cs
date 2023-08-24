using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingPath : MonoBehaviour
{
    public bool isCutting = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Knife" || other.name == "Handsaw")
        {
            isCutting = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Knife" || other.name == "Handsaw")
        {
            isCutting = false;
        }
    }
}