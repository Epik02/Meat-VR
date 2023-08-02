using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatMover : MonoBehaviour
{
    public int part;

    private List<GameObject> meats = new List<GameObject>();
    private Vector3 push;

    // Start is called before the first frame update
    void Start()
    {
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
        if (meats.Count > 0)
        {
            foreach (var item in meats)
            {
                item.transform.localPosition += push;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Meat"))
        {
            meats.Add(other.gameObject);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Meat"))
        {
            meats.Remove(other.gameObject);
        }
    }
}
