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
        if (inMeat)                         // Checks if slice object is in meat
        {
            if (path != null)               // If path exists then...
            {
                if (path.isCutting)         // If the slice object is in the accuracy path then it will add true
                {
                    cutPath.Add(true);
                }
                else                        // Otherwise it will add false
                {
                    cutPath.Add(false);
                }
                cutCount++;                 // Total amount of cuts per update
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Knife" || other.name == "Handsaw")   // Checks if slice object has entered the meat
        {
            inMeat = true;                                      // It is in the meat when slice object enters the meat
            path = FindAnyObjectByType<CuttingPath>();          // Will find the CuttingPath object in the scene (there will always be 1 active)
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Knife" || other.name == "Handsaw")   // Checks if slice object has exited the meat
        {
            inMeat = false;                                     // Then the slice object is no longer in the meat
            path = null;                                        // CuttingPath has been destroyed so it is now null
        }
    }

    // Calculates and returns the total accuracy of the complete cut
    public float CalculateAccuracy()
    {
        float overallAccuracy = 0;
        float correctHits = 0;
        float allHits = 0;

        for (int i = 0; i < cutCount; ++i)                      // Will go through the total amount of cuts per update
        {
            if (cutPath[i])                                     // If the slice object has accurately hit the guideline...
            {
                correctHits += 1;                               // Then total correct hits will be added
            }
            allHits += 1;                                       // Total hits in general will be added no matter what
        }

        overallAccuracy = (correctHits / allHits) * 100.0f;     // Calculate the overall accuracy into a percentage

        cutPath.Clear();                                        // Restart the process by deleting all the boolean values
        cutCount = 0;

        return overallAccuracy;                                 // Returns the calculated overall accuracy
    }
}
