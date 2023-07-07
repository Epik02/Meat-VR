using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KnifeStrength : MonoBehaviour
{
    public float strength = 20.0f;
    public TMP_Text textDisplay;

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
            textDisplay.text = "Strength: " + strength + "%";
        }
    }
}
