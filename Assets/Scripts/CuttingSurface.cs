using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingSurface : MonoBehaviour
{
    public enum KnifeAccuracy 
    {
        // Reset distance
        BAD, 
        // Good range
        GOOD,
        // Complete
        COMPLETE,
    }

    public KnifeAccuracy accuracy;
    public Guidelines guide;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "AccuracyTestKnife")
        {
            guide.OnKnifeEnter(accuracy);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "AccuracyTestKnife")
        {
            guide.OnKnifeExit(accuracy);
        }
    }
}
