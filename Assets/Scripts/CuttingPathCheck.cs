using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingPathCheck : MonoBehaviour
{

    public bool cuttingInPath = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "AccuracyTestKnife")
        {
            cuttingInPath = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "AccuracyTestKnife")
        {
            cuttingInPath = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
