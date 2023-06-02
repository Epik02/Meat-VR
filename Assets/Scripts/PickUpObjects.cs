using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class PickUpObjects : MonoBehaviour
{
    bool triggerValue;
    private float _gripStrength;
    private bool _grabbingActive = false;
    private UnityEngine.XR.InputDevice Oculus;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Oculus.TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out triggerValue) && triggerValue)
        {
            Debug.Log("TRIGGER WAS PRESSSED YESSSSSS");
        }

        //Oculus.TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out triggerValue);
        //UnityEngine.XR.XRInputSubsystem.

        //Debug.Log(triggerValue);

        //Oculus.TryGetFeatureValue(CommonUsages.grip, out _gripStrength);

        if (_gripStrength > 0.2f)
    {
        if (!_grabbingActive)
        {
            _grabbingActive = true;
            //press event
        }
 
 
    } else if (_grabbingActive)
    {
    _grabbingActive = false;
    //release event

}

    }
}
