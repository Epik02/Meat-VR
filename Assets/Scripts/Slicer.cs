using UnityEngine;
using EzySlice;
using TMPro;
public class Slicer : MonoBehaviour
{
    public float strength = 100.0f;
    public TMP_Text textDisplay;

    public Material materialAfterSlice;
    public LayerMask sliceMask;
    public bool isTouched;
    public VelocityEstimator velocityEstimator;

    public Transform startSlicePoint;
    public Transform endSlicePoint;

    private void Update()
    {
        textDisplay.text = "Strength: " + strength + "%";

        if (strength > 100)
        {
            strength = 100.0f;
        }

        if (isTouched == true)
        {
            isTouched = false;

            Collider[] objectsToBeSliced = Physics.OverlapBox(transform.position, new Vector3(1, 0.1f, 0.1f), transform.rotation, sliceMask);
            
            foreach (Collider objectToBeSliced in objectsToBeSliced)
            {
                SlicedHull slicedObject = SliceObject(objectToBeSliced.gameObject, materialAfterSlice);

                GameObject upperHullGameobject = slicedObject.CreateUpperHull(objectToBeSliced.gameObject, materialAfterSlice);
                GameObject lowerHullGameobject = slicedObject.CreateLowerHull(objectToBeSliced.gameObject, materialAfterSlice);

                upperHullGameobject.transform.position = objectToBeSliced.transform.position;
                lowerHullGameobject.transform.position = objectToBeSliced.transform.position;

                MakeItPhysical(upperHullGameobject);
                MakeItPhysical(lowerHullGameobject);

                //so you can cut the upper and lower hull
                int LayerSwitch = LayerMask.NameToLayer("Sliceable");
                upperHullGameobject.layer = LayerSwitch;
                lowerHullGameobject.layer = LayerSwitch;

                Destroy(objectToBeSliced.gameObject);
                strength -= 5.0f;
            }
        }
    }

    private void MakeItPhysical(GameObject obj)
    {
        obj.AddComponent<MeshCollider>().convex = true;
        obj.AddComponent<Rigidbody>();
        
        Softbody softbody = obj.AddComponent<Softbody>();
        //softbody.bounciness = 2.0f;
        //softbody.stiffness = 2.0f;
        softbody.force = 2.0f;
    }

    private SlicedHull SliceObject(GameObject obj, Material crossSectionMaterial = null)
    {
        return obj.Slice(transform.position, transform.up, crossSectionMaterial);
    }


}
