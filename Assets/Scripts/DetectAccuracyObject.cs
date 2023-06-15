using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DetectAccuracyObject : MonoBehaviour
{

    private int numOfHits = 0;
    private int numOfMiss = 0;
    private float overallAccuracy;
    public TextMeshPro mText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "AccuracyObject")
        {
            numOfHits = numOfHits + 1;
            Debug.Log("Hit");
        }
        else
        {
            numOfMiss = numOfMiss + 1;
            Debug.Log("Miss");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "AccuracyObject")
        {
            overallAccuracy = numOfHits / (numOfHits + numOfMiss);
            overallAccuracy = overallAccuracy * 100;
            //mText.text = "Accuracy: " + overallAccuracy;
            numOfHits = 0;
            numOfMiss = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
