using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingSurface : MonoBehaviour
{
    public enum KnifeAccuracy 
    {
        // Reset
        BAD, 
        // Good
        GOOD,
        // Complete
        COMPLETE,
    }

    public KnifeAccuracy accuracy;
    public Guidelines guide;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == guide.item.name)      // Notify guidelines of slice object entering
        {
            guide.OnKnifeEnter(accuracy);       // Then allow cutting
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == guide.item.name)      // Notify guidelines of slice object exiting
        {
            guide.OnKnifeExit(accuracy);        // Then allow cutting
        }
    }
}
