using Autohand;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MeatPlacer : MonoBehaviour
{
    public GameObject lineRenderer;
    public GameObject guideline;
    public Transform meatSpot;
    public GameObject currentMeat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentMeat != null)
        {
            lineRenderer.SetActive(true);
            guideline.SetActive(true);
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;

            currentMeat.transform.position = meatSpot.position;
            //currentMeat.transform.rotation = meatSpot.rotation;
            currentMeat.GetComponent<MeshCollider>().isTrigger = true;
            currentMeat.GetComponent<Grabbable>().enabled = false;

            Rigidbody rb = currentMeat.GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.velocity = new Vector3(0, 0, 0);
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        else
        {
            lineRenderer.SetActive(false);
            guideline.SetActive(false);
            GetComponent<MeshRenderer>().enabled = true;
            GetComponent<Collider>().enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Meat"))
        {
            currentMeat = other.gameObject;
        }
    }
}
