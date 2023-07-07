using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRespawn : MonoBehaviour
{
    public Transform newPosition;
    public float bottom = -1.0f;

    private Dirty dirty;

    // Start is called before the first frame update
    void Start()
    {
        dirty = GetComponent<Dirty>();
        if (dirty == null)
        {
            dirty = GetComponentInChildren<Dirty>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= bottom)
        {
            transform.position = newPosition.position;
            transform.rotation = newPosition.rotation;
            dirty.dirtiness = 100.0f;
        }
    }
}
