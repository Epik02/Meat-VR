using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gloves : MonoBehaviour
{
    public GameObject rightHand;
    public GameObject leftHand;
    public Mesh gloveMesh;
    public Material gloveMaterial;

    private SkinnedMeshRenderer rightHandMesh;
    private SkinnedMeshRenderer leftHandMesh;

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

    private void OnCollisionEnter(Collision other)
    {
        if (rightHand.GetComponentInChildren<Dirty>().dirtiness <= 0.0f && leftHand.GetComponentInChildren<Dirty>().dirtiness <= 0.0f &&
            rightHand.GetComponentInChildren<Clean>().cleanness <= 0.0f && leftHand.GetComponentInChildren<Clean>().cleanness <= 0.0f &&
            rightHand.GetComponentInChildren<Wet>().wetness <= 0.0f && leftHand.GetComponentInChildren<Wet>().wetness <= 0.0f)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                rightHandMesh.sharedMesh = gloveMesh;
                rightHandMesh.material = gloveMaterial;

                leftHandMesh.sharedMesh = gloveMesh;
                leftHandMesh.material = gloveMaterial;

                Destroy(rightHand.GetComponentInChildren<Dirty>());
                Destroy(rightHand.GetComponentInChildren<Clean>());
                Destroy(rightHand.GetComponentInChildren<Wet>());
                Destroy(leftHand.GetComponentInChildren<Dirty>());
                Destroy(leftHand.GetComponentInChildren<Clean>());
                Destroy(leftHand.GetComponentInChildren<Wet>());

                Destroy(gameObject);
            }
        }
    }
}
