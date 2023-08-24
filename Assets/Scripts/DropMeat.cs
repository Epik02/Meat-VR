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
        for (int i = 0; i < meshRenderer.materials.Length; i++)     // Take each skinned mesh renderer material
        {
            if (meshRenderer.materials[i].name.Contains("Dirt"))    // If material has dirt in it then...
            {
                mat = i;                                            // That is the material we will change transparency
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= bottom)                 // If meat has reached the bottom then...
        {
            transform.position = respawnPosition.position;  // Change the position of the meat to the respawn position
            transform.rotation = respawnPosition.rotation;  // Change the rotation of the meat to the respawn rotation
            rb.velocity = Vector3.zero;                     // Reset the velocity (so it does not come crashing back down)
            if (ScoreManager.instance)                      // If the score manager exists...
            {
                ScoreManager.instance.RemoveScore(10);      // Remove 10 points from the total score
            }
            dropped = true;                                 // Meat dropped is true
        }
        if (dropped)                                        // If the meat has been dropped...
        {
            Color c = meshRenderer.materials[mat].color;    // Creates a new color from the dirt material
            c.a = 1.0f;                                     // Changes the alpha of the new color
            meshRenderer.materials[mat].color = c;          // Puts the new color back into the original materials spot
        }
        else                                                // Otherwise...
        {
            Color c = meshRenderer.materials[mat].color;    // Same proccess but different alpha value
            c.a = 0.0f;
            meshRenderer.materials[mat].color = c;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Trash") && dropped)            // If meat collides with the trash can...
        {
            if (ScoreManager.instance)                                  // If score manager exists...
            {
                ScoreManager.instance.AddScore(5);                      // Add 5 points to the total score
            }
            if (!tutorial)                                              // If tutorial is false...
            {
                GameObject newMeat = Instantiate(gameObject);           // Duplicate meat object
                newMeat.transform.position = respawnPosition.position;  // Change the position to the respawn position
                newMeat.transform.rotation = respawnPosition.rotation;  // Change the rotation to the respawn rotation
            }
            Destroy(gameObject);                                        // Destroy original meat
        }
    }
}
