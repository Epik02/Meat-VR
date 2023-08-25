using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gloves : MonoBehaviour
{
    public GameObject rightHand;
    public GameObject leftHand;
    public Mesh gloveMesh;
    public Material gloveMaterial;
    public ParticleSystem gloveParticles;

    private GameObject rightModel;
    private GameObject leftModel;
    private SkinnedMeshRenderer rightHandMesh;
    private SkinnedMeshRenderer leftHandMesh;
    private bool onHands = true;

    // Start is called before the first frame update
    void Start()
    {
        rightHandMesh = rightHand.GetComponentInChildren<SkinnedMeshRenderer>();
        leftHandMesh = leftHand.GetComponentInChildren<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        // When the gloves are showing again then the gloves will become dirty
        if (rightModel != null && leftModel != null) 
        {
            if (rightModel.GetComponent<DirtyGloves>() && leftModel.GetComponent<DirtyGloves>())
            {
                rightModel.GetComponent<DirtyGloves>().dirtiness = 100;
                leftModel.GetComponent<DirtyGloves>().dirtiness = 100;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // If player is on the hands step and the right and left hand have been fully cleaned...
        if (onHands)
        {
            if (rightHand.GetComponentInChildren<Dirty>().dirtiness <= 0.0f && leftHand.GetComponentInChildren<Dirty>().dirtiness <= 0.0f &&
                rightHand.GetComponentInChildren<Clean>().cleanness <= 0.0f && leftHand.GetComponentInChildren<Clean>().cleanness <= 0.0f &&
                rightHand.GetComponentInChildren<Wet>().wetness <= 0.0f && leftHand.GetComponentInChildren<Wet>().wetness <= 0.0f)
            {
                if (other.gameObject.CompareTag("Player"))                  // If the other object is a hand...
                {
                    rightHandMesh.sharedMesh = gloveMesh;                   // Switch from hand to glove mesh
                    rightHandMesh.material = gloveMaterial;                 // Switch from hand to glove material

                    leftHandMesh.sharedMesh = gloveMesh;
                    leftHandMesh.material = gloveMaterial;

                    rightModel = rightHand.GetComponentInChildren<Dirty>().gameObject;
                    leftModel = leftHand.GetComponentInChildren<Dirty>().gameObject;

                    // Add dirty gloves component for rotten meat pickup
                    if (!rightModel.GetComponent<DirtyGloves>() && !leftModel.GetComponent<DirtyGloves>())
                    {
                        rightModel.AddComponent<DirtyGloves>();
                        leftModel.AddComponent<DirtyGloves>();
                    }

                    // Removes components for each hand
                    Destroy(rightHand.GetComponentInChildren<Dirty>());
                    Destroy(rightHand.GetComponentInChildren<Clean>());
                    Destroy(rightHand.GetComponentInChildren<Wet>());
                    Destroy(leftHand.GetComponentInChildren<Dirty>());
                    Destroy(leftHand.GetComponentInChildren<Clean>());
                    Destroy(leftHand.GetComponentInChildren<Wet>());

                    gameObject.SetActive(false);
                    onHands = false;
                }
            }
        }

        if (rightModel.GetComponent<DirtyGloves>() && leftModel.GetComponent<DirtyGloves>())
        {
            if (other.gameObject.CompareTag("Player"))
            {
                // Reset gloves
                rightModel.GetComponent<DirtyGloves>().dirtiness = 0;
                leftModel.GetComponent<DirtyGloves>().dirtiness = 0;
                gloveParticles.Play();
                gameObject.SetActive(false);
            }
        }
    }
}
