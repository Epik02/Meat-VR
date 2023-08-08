using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class SliceObject : MonoBehaviour
{
    [Header("Slice Elements")]
    public Transform startSlicePoint;
    public Transform endSlicePoint;
    public VelocityEstimator velocityEstimator;
    public LayerMask sliceableLayer;

    [Space]
    [Header("Cut Elements")]
    public Material cutMaterial;
    public float cutForce = 2000f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool hasHit = Physics.Linecast(startSlicePoint.position, endSlicePoint.position, out RaycastHit hit, sliceableLayer);
        if (hasHit && GetComponent<KnifeStraighten>().strength > 0.0f) 
        {
            GameObject target = hit.transform.gameObject;
            Slice(target);
        }
    }

    public void Slice(GameObject target)
    {
        Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        Vector3 normal = Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity);
        normal.Normalize();

        SlicedHull hull = target.Slice(endSlicePoint.position, normal);

        if (hull != null)
        {
            GameObject upperHull = hull.CreateUpperHull(target, cutMaterial);
            SetupSlicedObject(upperHull);

            GameObject lowerHull = hull.CreateLowerHull(target, cutMaterial);
            SetupSlicedObject(lowerHull);

            GetComponent<KnifeStraighten>().strength -= 5.0f;

            Destroy(target);
        }
    }

    public void SetupSlicedObject(GameObject slicedObject)
    {
        CutMeat cm = slicedObject.AddComponent<CutMeat>();
        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        collider.convex = true;
        rb.AddExplosionForce(cutForce, slicedObject.transform.position, 1f);
        int LayerSwitch = LayerMask.NameToLayer("Sliceable");
        slicedObject.layer = LayerSwitch;
        slicedObject.tag = "Meat";
    }
}
