using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using Autohand;

public class Slice : MonoBehaviour
{
    [Header("Slice Elements")]
    public Transform startSlicePoint;
    public Transform endSlicePoint;
    public VelocityEstimator velocityEstimator;
    public LayerMask sliceableLayer;

    [Header("Cut Elements")]
    public Transform meatSpawn;
    public Material dirtMaterial;
    public Material cutMaterial;
    public float cutForce = 2000f;

    [Header("Skirts")]
    public List<GameObject> skirts = new List<GameObject>();

    [Header("Tutorial Options")]
    public bool tutorial = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    // Slices an object from a plane
    public void SlicePlane(GameObject target, Transform plane, bool freeze)
    {
        if (skirts.Count > 0)                                                       // If there are still skirts in the meat then remove them
        {
            DeSkirt();
        }
        else                                                                        // If there are no skirts in the meat then do a normal slice
        {
            SlicedHull hull = target.Slice(plane.position, plane.up);               // Slice the meat by the plane position

            if (hull != null)
            {
                // Create and setup the upper hull of the meat
                GameObject upperHull = hull.CreateUpperHull(target, cutMaterial);
                SetupSlicedObject(upperHull, "Sliceable", "Meat");
                if (freeze) { Freeze(upperHull); }
                else { SetupDropMeat(upperHull); }

                // Create and setup the lower hull of the meat
                GameObject lowerHull = hull.CreateLowerHull(target, cutMaterial);
                SetupSlicedObject(lowerHull, "Untagged", "Default");

                // Delete the original meat
                Destroy(target);
            }
        }
    }

    // Slices an object from a plane without other steps
    public void SliceBasic(GameObject target, Transform plane)
    {
        SlicedHull hull = target.Slice(plane.position, plane.up);

        if (hull != null)
        {
            GameObject upperHull = hull.CreateUpperHull(target, cutMaterial);
            SetupSlicedObject(upperHull, "Sliceable", "Meat");

            GameObject lowerHull = hull.CreateLowerHull(target, cutMaterial);
            SetupSlicedObject(lowerHull, "Sliceable", "Meat");

            Destroy(target);
        }
    }

    // Sets up components of the sliced hull
    public void SetupSlicedObject(GameObject slicedObject, string layer, string tag)
    {
        CutMeat cm = slicedObject.AddComponent<CutMeat>();
        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
        Grabbable grabbable = slicedObject.AddComponent<Grabbable>();
        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        collider.convex = true;
        rb.AddExplosionForce(cutForce, slicedObject.transform.position, 1f);
        int LayerSwitch = LayerMask.NameToLayer(layer);
        if (tag != "Default")
        {
            slicedObject.layer = LayerSwitch;
            slicedObject.tag = tag;
        }
        if (tag == "Meat")
        {
            AccuracySystem accuracySystem = slicedObject.AddComponent<AccuracySystem>();
        }
        else
        {
            SetupDropMeat(slicedObject);
        }
    }

    // Sets up dropped meat component for sliced hull
    public void SetupDropMeat(GameObject target)
    {
        DropMeat dm = target.AddComponent<DropMeat>();
        dm.respawnPosition = meatSpawn;
        dm.bottom = -0.35f;

        MeshRenderer meshRenderer = target.GetComponent<MeshRenderer>();
        List<Material> tempMaterials = new List<Material>();
        foreach (var item in meshRenderer.materials)
        {
            tempMaterials.Add(item);
        }
        tempMaterials.Add(dirtMaterial);
        meshRenderer.materials = tempMaterials.ToArray();
    }

    // Freezes sliced hull
    public void Freeze(GameObject target)
    {
        target.GetComponent<MeshCollider>().isTrigger = true;
        Destroy(target.GetComponent<Rigidbody>());
        Destroy(target.GetComponent<Grabbable>());
    }

    // Removes the skirt of the meat
    public void DeSkirt()
    {
        skirts[0].transform.parent = null;
        skirts[0].AddComponent<Rigidbody>();
        skirts[0].GetComponent<MeshCollider>().isTrigger = false;
        skirts[0].GetComponent<Rigidbody>().velocity = Vector3.zero;
        skirts[0].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        skirts[0].AddComponent<Grabbable>();
        SetupDropMeat(skirts[0]);
        skirts.RemoveAt(0);
    }
}
