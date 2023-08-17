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

    //for accuracy on cut objects
    public GameObject accObject;
    public GameObject objToIns;
    public TextMeshPro text;
    public GameObject knife;

    public Transform meatRespawnTransform;

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

        if (isTouched /*&& knife.GetComponent<KnifeStraighten>().strength > 0.0f*/)
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

                //make it so cut pieces of meat work with the accuracy system
                upperHullGameobject.AddComponent<Accuracy>();
                lowerHullGameobject.AddComponent<Accuracy>();

                upperHullGameobject.AddComponent<CutMeat>();
                lowerHullGameobject.AddComponent<CutMeat>();

                if (meatRespawnTransform != null)
                {
                    upperHullGameobject.AddComponent<DropMeat>();
                    upperHullGameobject.GetComponent<DropMeat>().respawnPosition = meatRespawnTransform;
                    upperHullGameobject.GetComponent<DropMeat>().bottom = -0.5f;
                    lowerHullGameobject.AddComponent<DropMeat>();
                    lowerHullGameobject.GetComponent<DropMeat>().respawnPosition = meatRespawnTransform;
                    lowerHullGameobject.GetComponent<DropMeat>().bottom = -0.5f;
                }

                upperHullGameobject.GetComponent<Accuracy>().accuracyObject = accObject;
                lowerHullGameobject.GetComponent<Accuracy>().accuracyObject = accObject;

                upperHullGameobject.GetComponent<Accuracy>().mText = text;
                lowerHullGameobject.GetComponent<Accuracy>().mText = text;

                upperHullGameobject.GetComponent<Accuracy>().Knife = knife;
                lowerHullGameobject.GetComponent<Accuracy>().Knife = knife;

                upperHullGameobject.GetComponent<Accuracy>().objectToInstantiate = objToIns;
                lowerHullGameobject.GetComponent<Accuracy>().objectToInstantiate = objToIns;

                upperHullGameobject.GetComponent<Accuracy>().speed = 1;
                lowerHullGameobject.GetComponent<Accuracy>().speed = 1;
                upperHullGameobject.GetComponent<Accuracy>().timeBetweenInstantiation = 0.1f;
                lowerHullGameobject.GetComponent<Accuracy>().timeBetweenInstantiation = 0.1f;



                Grabbable upperHullGrabbable = upperHullGameobject.GetComponent<Grabbable>();
                Grabbable lowerHullGrabbable = lowerHullGameobject.GetComponent<Grabbable>();

                upperHullGrabbable.body = upperHullGameobject.transform.GetComponent<Rigidbody>();
                lowerHullGrabbable.body = lowerHullGameobject.transform.GetComponent<Rigidbody>();

                //so you can cut the upper and lower hull
                int LayerSwitch = LayerMask.NameToLayer("Sliceable");
                upperHullGameobject.layer = LayerSwitch;
                lowerHullGameobject.layer = LayerSwitch;

                Destroy(objectToBeSliced.gameObject);
                knife.GetComponent<KnifeStraighten>().strength -= 10.0f;
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
