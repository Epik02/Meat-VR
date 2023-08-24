using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KnifeStraighten : MonoBehaviour
{
    public float strength = 20.0f;
    public TMP_Text textDisplay;
    public bool correctAngle = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (strength > 100.0f)                                      // Make sure the strength does not go above 100
        {
            strength = 100.0f;
        }
        if (textDisplay != null)
        {
            textDisplay.text = "Straighten: " + strength + "%";     // Update the straighten text
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Honer")                                  // If the honer has entered the trigger...
        {
            correctAngle = true;                                    // Then it is on the correct angle
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Honer")
        {
            correctAngle = false;
        }
    }
}
