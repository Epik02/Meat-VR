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
        if (tutorial)
        {
            bool hasHit = Physics.Linecast(startSlicePoint.position, endSlicePoint.position, out RaycastHit hit, sliceableLayer);
            if (hasHit && GetComponent<KnifeStraighten>().strength > 0.0f)
            {
                GameObject target = hit.transform.gameObject;
                SliceObject(target);
            }
        }
    }

    public void SlicePlane(GameObject target, Transform plane, bool freeze)
    {
        if (skirts.Count > 0)
        {
            DeSkirt();
        }
        else
        {
            SlicedHull hull = target.Slice(plane.position, plane.up);

            if (hull != null)
            {
                GameObject upperHull = hull.CreateUpperHull(target, cutMaterial);
                SetupSlicedObject(upperHull, "Sliceable", "Meat");
                if (freeze) { Freeze(upperHull); }

                GameObject lowerHull = hull.CreateLowerHull(target, cutMaterial);
                SetupSlicedObject(lowerHull, "Grabbable", "Finish");

                Destroy(target);
            }
        }
    }

    public void SliceObject(GameObject target)
    {
        Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        Vector3 normal = Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity);
        normal.Normalize();

        SlicedHull hull = target.Slice(endSlicePoint.position, normal);

        if (hull != null)
        {
            GameObject upperHull = hull.CreateUpperHull(target, cutMaterial);
            SetupSlicedObject(upperHull, "Sliceable", "Meat");

            GameObject lowerHull = hull.CreateLowerHull(target, cutMaterial);
            SetupSlicedObject(lowerHull, "Grabbable", "Finish");

            Destroy(target);
        }
    }

    public void SetupSlicedObject(GameObject slicedObject, string layer, string tag)
    {
        CutMeat cm = slicedObject.AddComponent<CutMeat>();
        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
        Grabbable grabbable = slicedObject.AddComponent<Grabbable>();
        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        collider.convex = true;
        rb.AddExplosionForce(cutForce, slicedObject.transform.position, 1f);
        int LayerSwitch = LayerMask.NameToLayer(layer);
        slicedObject.layer = LayerSwitch;
        slicedObject.tag = tag;
        if (tag == "Meat")
        {
            AccuracySystem accuracySystem = slicedObject.AddComponent<AccuracySystem>();
        }
    }

    public void Freeze(GameObject target)
    {
        target.GetComponent<MeshCollider>().isTrigger = true;
        Destroy(target.GetComponent<Rigidbody>());
        Destroy(target.GetComponent<Grabbable>());
    }

    public void DeSkirt()
    {
        skirts[0].transform.parent = null;
        skirts[0].GetComponent<MeshCollider>().isTrigger = false;
        skirts[0].AddComponent<Rigidbody>();
        skirts[0].AddComponent<Grabbable>();
        skirts.RemoveAt(0);
    }
}
