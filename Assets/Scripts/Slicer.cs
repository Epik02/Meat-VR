using UnityEngine;
using EzySlice;
using TMPro;
using Autohand;

public class Slicer : MonoBehaviour
{
    public float strength = 100.0f;
    //public TMP_Text textDisplay;

    public Material materialAfterSlice;
    public LayerMask sliceMask;
    public bool isTouched;
    public VelocityEstimator velocityEstimator;

    public Transform startSlicePoint;
    public Transform endSlicePoint;

    private void Update()
    {
        //if (textDisplay != null)
        //{
        //    textDisplay.text = "Strength: " + strength + "%";
        //}

        //if (strength > 100)
        //{
        //    strength = 100.0f;
        //}

        if (isTouched == true)
        {
            isTouched = false;

            Collider[] objectsToBeSliced = Physics.OverlapBox(transform.position, new Vector3(1, 0.1f, 0.1f), transform.rotation, sliceMask);
            
            foreach (Collider objectToBeSliced in objectsToBeSliced)
            {
                SlicedHull slicedObject = SliceObject(objectToBeSliced.gameObject, materialAfterSlice);

                GameObject upperHullGameobject = slicedObject.CreateUpperHull(objectToBeSliced.gameObject, materialAfterSlice);
                GameObject lowerHullGameobject = slicedObject.CreateLowerHull(objectToBeSliced.gameObject, materialAfterSlice);

                //test
                //upperHullGameobject.transform.parent = objectToBeSliced.transform.GetChild(0);

                upperHullGameobject.transform.position = objectToBeSliced.transform.position;
                lowerHullGameobject.transform.position = objectToBeSliced.transform.position;

                upperHullGameobject.tag = "cuttable";
                lowerHullGameobject.tag = "cuttable";

                MakeItPhysical(upperHullGameobject);
                MakeItPhysical(lowerHullGameobject);

                //makes it so you can pick up the sliced objects
                upperHullGameobject.AddComponent<Grabbable>();
                lowerHullGameobject.AddComponent<Grabbable>();

                Grabbable upperHullGrabbable = upperHullGameobject.GetComponent<Grabbable>();
                Grabbable lowerHullGrabbable = lowerHullGameobject.GetComponent<Grabbable>();

                upperHullGrabbable.body = upperHullGameobject.transform.GetComponent<Rigidbody>();
                lowerHullGrabbable.body = lowerHullGameobject.transform.GetComponent<Rigidbody>();

                //so you can cut the upper and lower hull
                int LayerSwitch = LayerMask.NameToLayer("Sliceable");
                upperHullGameobject.layer = LayerSwitch;
                lowerHullGameobject.layer = LayerSwitch;

                Destroy(objectToBeSliced.gameObject);
                //strength -= 5.0f;
            }
        }
    }

    private void MakeItPhysical(GameObject obj)
    {
        obj.AddComponent<MeshCollider>().convex = true;
        obj.AddComponent<Rigidbody>();
        
        //Softbody softbody = obj.AddComponent<Softbody>();

        //softbody.bounciness = 2.0f;
        //softbody.stiffness = 2.0f;

        //softbody.force = 2.0f;
    }

    private SlicedHull SliceObject(GameObject obj, Material crossSectionMaterial = null)
    {
        return obj.Slice(transform.position, transform.up, crossSectionMaterial);
    }


}
