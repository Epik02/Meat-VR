using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropMeat : MonoBehaviour
{
    public Transform droppedPosition;
    public Transform respawnPosition;
    public float bottom = -0.6f;

    private bool dropped;
    private int mat;
    private MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        dropped = false;
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
            transform.position = droppedPosition.position;
            transform.rotation = droppedPosition.rotation;
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
            GameObject newMeat = Instantiate(gameObject);
            newMeat.transform.position = respawnPosition.position;
            newMeat.transform.rotation = respawnPosition.rotation;
            Destroy(gameObject);
        }
    }
}
