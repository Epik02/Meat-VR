using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knifeMechanics : MonoBehaviour
{
    bool inMeatInGeneral = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "cuttable")
        {
            inMeatInGeneral = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "cuttable")
        {
            inMeatInGeneral = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inMeatInGeneral)
        {
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
            //this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;
            //this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;
            this.GetComponent<Rigidbody>().freezeRotation = true;

        }
        else
        {
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }
}
