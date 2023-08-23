using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccuracySystem : MonoBehaviour
{
    private CuttingPath path;
    private bool inMeat;
    private int cutCount = 0;
    private List<bool> cutPath = new List<bool>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inMeat)
        {
            if (path != null)
            {
                if (path.isCutting)
                {
                    cutPath.Add(true);
                }
                else
                {
                    cutPath.Add(false);
                }
                cutCount++;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Knife" || other.name == "Handsaw")
        {
            inMeat = true;
            path = FindAnyObjectByType<CuttingPath>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Knife" || other.name == "Handsaw")
        {
            inMeat = false;
            path = null;
        }
    }

    public float CalculateAccuracy()
    {
        float overallAccuracy = 0;
        float correctHits = 0;
        float allHits = 0;

        for (int i = 0; i < cutCount; ++i)
        {
            if (cutPath[i])
            {
                correctHits += 1;
            }
            allHits += 1;
        }

        overallAccuracy = (correctHits / allHits) * 100.0f;

        cutPath.Clear();
        cutCount = 0;

        return overallAccuracy;
    }
}
