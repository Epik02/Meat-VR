using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand;
using System;

public class MeatManager : MonoBehaviour
{
    public Vector3 position;
    public Quaternion rotation;
    public GameObject accuracyObject;
    private Collider currentMeat;
    private bool inMeat = false;
    private bool isDirty = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        currentMeat = other;
        MeshRenderer mr = other.GetComponent<MeshRenderer>();
        if (mr != null) 
        {
            isDirty = false;
            for (int i = 0; i < mr.materials.Length; i++)
            {
                if (mr.materials[i].name.Contains("Dirt") && mr.materials[i].color.a > 0.0f)
                {
                    isDirty = true;
                }
            }
        }

        if (other.gameObject.tag == "cuttable" && !isDirty)
        {
            inMeat = true;
            accuracyObject.SetActive(true);
            other.gameObject.transform.position = position;

            other.gameObject.GetComponent<MeshCollider>().isTrigger = true;
            other.gameObject.GetComponent<Grabbable>().enabled = false;

            other.gameObject.layer = 8;

            other.gameObject.GetComponent<Rigidbody>().useGravity = false;

            this.transform.position = new Vector3(100, 100, 100); //removes sphere from scene temporaraly
        }
        else
        {
            inMeat = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inMeat && currentMeat != null)
        {
            currentMeat.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            currentMeat.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
            currentMeat.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
            currentMeat.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
            currentMeat.gameObject.transform.rotation = Quaternion.Euler(-73f, -84f, 49);
            currentMeat.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;
            currentMeat.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY;
            currentMeat.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;
        }
        else
        {
            accuracyObject.SetActive(false);
            this.transform.position = new Vector3(2.293f, 0.232f, 1.204f); //brings green sphere back to scene
        }
    }
}
