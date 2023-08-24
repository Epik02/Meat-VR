using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatMover : MonoBehaviour
{
    public int part;

    private Vector3 push;
    private bool moveMeat = false;

    // Start is called before the first frame update
    void Start()
    {
        // The movement of the mover is different depending on the part
        switch (part)
        {
            case 0:
                push = new Vector3(0.0f, 0.0f, -0.005f);
                break;
            case 1:
                push = new Vector3(0.0f, 0.0f, -0.05f);
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var tempMeatList = GameObject.FindGameObjectsWithTag("Meat");

        if (moveMeat)                                       // Checks to see if meat is on mover
        {
            foreach (var item in tempMeatList)
            {
                item.transform.localPosition += push;       // Push meat on mover
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Meat"))
        {
            moveMeat = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Meat"))
        {
            moveMeat = false;
        }
    }
}
