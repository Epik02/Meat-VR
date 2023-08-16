using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

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
        if (strength > 100.0f)
        {
            strength = 100.0f;
        }
        if (textDisplay != null)
        {
            textDisplay.text = "Straighten: " + strength + "%";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Honer")
        {
            correctAngle = true;
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
