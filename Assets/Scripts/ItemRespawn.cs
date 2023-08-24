using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRespawn : MonoBehaviour
{
    public Transform newPosition;
    public float bottom = -0.6f;

    private Dirty dirty;
    private Clean clean;
    private Wet wet;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        dirty = GetComponent<Dirty>();
        if (dirty == null)                              // If object does not have dirty as a main component...
        {
            dirty = GetComponentInChildren<Dirty>();    // Get the dirty component in there children
        }

        clean = GetComponent<Clean>();                  // If object does not have clean as a main component...
        if (clean == null)
        {
            clean = GetComponentInChildren<Clean>();    // Get the clean component in there children
        }

        wet = GetComponent<Wet>();                      // If object does not have wet as a main component...
        if (wet == null)
        {
            wet = GetComponentInChildren<Wet>();        // Get the wet component in there children
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= bottom)                 // When the position of the item has hit the bottom...
        {
            transform.position = newPosition.position;      // Change position to the spawn position
            transform.rotation = newPosition.rotation;      // Change the rotation to the spawn rotation
            rb.velocity = Vector3.zero;                     // Reset the velocity
            if (dirty && clean && wet)                      // If item has these components...
            {                                               // Make the item dirty
                dirty.dirtiness = 100.0f; 
                clean.cleanness = 0.0f;
                wet.wetness = 0.0f;
                if (ScoreManager.instance)                  // If score manager exists...
                {
                    ScoreManager.instance.RemoveScore(10);  // Remove 10 points from the total score
                }
            }
        }
    }
}
