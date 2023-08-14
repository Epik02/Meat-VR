using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropMeat : MonoBehaviour
{
    public Transform respawnPosition;
    public float bottom = -0.6f;
    public bool tutorial = false;

    private bool dropped;
    private int mat;
    private MeshRenderer meshRenderer;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        dropped = false;
        rb = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        for (int i = 0; i < meshRenderer.materials.Length; i++)
        {
            if (meshRenderer.materials[i].name.Contains("Dirt"))
            {
                mat = i;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= bottom)
        {
            transform.position = respawnPosition.position;
            transform.rotation = respawnPosition.rotation;
            rb.velocity = Vector3.zero;
            dropped = true;
        }
        if (dropped)
        {
            Color c = meshRenderer.materials[mat].color;
            c.a = 1.0f;
            meshRenderer.materials[mat].color = c;
        }
        else
        {
            Color c = meshRenderer.materials[mat].color;
            c.a = 0.0f;
            meshRenderer.materials[mat].color = c;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Trash") && dropped)
        {
            if (!tutorial)
            {
                GameObject newMeat = Instantiate(gameObject);
                newMeat.transform.position = respawnPosition.position;
                newMeat.transform.rotation = respawnPosition.rotation;
            }
            Destroy(gameObject);
        }
    }
}
