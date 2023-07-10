using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand;

public class MeatManager : MonoBehaviour
{
    public Vector3 position;
    public Quaternion rotation;
    private Collider currentMeat;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        currentMeat = other;
        if (other.gameObject.tag == "cuttable")
        {
            other.gameObject.transform.position = position;

            other.gameObject.GetComponent<MeshCollider>().isTrigger = true;
            other.gameObject.GetComponent<Grabbable>().enabled = false;

            other.gameObject.layer = 8;

            //other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
            //other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
            //other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;

            //other.gameObject.GetComponent<Rigidbody>().detectCollisions = false;
            other.gameObject.GetComponent<Rigidbody>().useGravity = false;
            //other.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentMeat.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        currentMeat.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
        currentMeat.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
        currentMeat.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
        currentMeat.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;
        currentMeat.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY;
        currentMeat.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;
    }
}
