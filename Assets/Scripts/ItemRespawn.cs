using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRespawn : MonoBehaviour
{
    public Transform newPosition;
    public float bottom = -0.6f;

    private Dirty dirty;
    private Clean clean;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        dirty = GetComponent<Dirty>();
        if (dirty == null)
        {
            dirty = GetComponentInChildren<Dirty>();
        }

        clean = GetComponent<Clean>();
        if (clean == null)
        {
            clean = GetComponentInChildren<Clean>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= bottom)
        {
            transform.position = newPosition.position;
            transform.rotation = newPosition.rotation;
            rb.velocity = Vector3.zero;
            if (dirty && clean)
            {
                dirty.dirtiness = 100.0f;
                clean.cleanness = 0.0f;
                if (ScoreManager.instance)
                {
                    ScoreManager.instance.RemoveScore(10);
                }
            }
        }
    }
}
