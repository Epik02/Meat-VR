using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingPath : MonoBehaviour
{
    public bool isCutting = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Knife" || other.name == "Handsaw")       // If a slice object has entered the accuracy path then...
        {
            isCutting = true;                                       // Cutting is true
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Knife" || other.name == "Handsaw")       // If a slice object has exited the accuracy path then...
        {
            isCutting = false;                                      // Cutting is false
        }
    }
}
