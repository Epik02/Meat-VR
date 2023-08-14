using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand.Demo;

public class PlayerConnected : MonoBehaviour
{
    //public XRHandControllerLink controller;
    public bool connected = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(controller.GetAxis2D(Common2DAxis.primaryAxis));
        // Right x = 0.1f
        // Left x = -0.1f
        // Up y = 0.1f
        // Down y = -0.1f

        //Debug.Log(controller.GetAxis2D(Common2DAxis.primaryAxis).x);
        // Right = 0.1f
        // Left = -0.1f
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "AutoHandPlayer")
        {
            connected = true;
        }
    }
}
