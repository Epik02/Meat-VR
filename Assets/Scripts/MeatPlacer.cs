using Autohand;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (currentMeat != null)                                        // If there is a current meat in the placer...
        {
            lineRenderer.SetActive(true);                               // Show the line renderer
            guideline.SetActive(true);                                  // Show the guideline
            GetComponent<MeshRenderer>().enabled = false;               // Hide the mesh of the placer
            GetComponent<Collider>().enabled = false;                   // Disable the collider of the placer

            currentMeat.transform.position = meatSpot.position;         // Set the current meat position to the meat spot
            currentMeat.GetComponent<MeshCollider>().isTrigger = true;  // Set the trigger on for the collider
            currentMeat.GetComponent<Grabbable>().enabled = false;      // Disable the grabbable from the object

            // Freeze the rigidbody
            Rigidbody rb = currentMeat.GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.velocity = new Vector3(0, 0, 0);
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        else                                                            // If there is no meat in the placer...
        {
            lineRenderer.SetActive(false);                              // Hide the line renderer
            guideline.SetActive(false);                                 // Hide the guideline
            GetComponent<MeshRenderer>().enabled = true;                // Show the placer
            GetComponent<Collider>().enabled = true;                    // Enable the collider
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
