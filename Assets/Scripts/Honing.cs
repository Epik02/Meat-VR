using Autohand;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class Honing : MonoBehaviour
{
    public List<HoningCheckpoint> checkpoints;
    public bool check = false;
    public LayerMask knifeLayer;

    private Rigidbody rb;
    private GameObject knifeObject;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        foreach (var honing in checkpoints)
        {
            if (honing.check)
            {
                i++;
            }

            if (i == checkpoints.Count && check)
            {
                if (gameObject.GetComponent<Dirty>().dirtiness > 0.0f)
                {
                    knifeObject.GetComponentInChildren<Dirty>().dirtiness += 10.0f;
                }
                else
                {
                    if (knifeObject.GetComponent<KnifeStrength>())
                    {
                        knifeObject.GetComponent<KnifeStrength>().strength += 10.0f;
                    }
                }

                honing.check = false;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (rb == null)
        {
            return;
        }

        if (knifeLayer == (knifeLayer | (1 << other.gameObject.layer)))
        {
            if (other.collider.attachedRigidbody == null || other.collider.attachedRigidbody.mass > 0.0000001f)
            {
                knifeObject = other.gameObject;
                check = true;
            }
        }
    }

    private void OnCollisionExit(Collision other) 
    {
        if (rb == null)
        {
            return;
        }

        if (knifeLayer == (knifeLayer | (1 << other.gameObject.layer)))
        {
            if (other.collider.attachedRigidbody == null || other.collider.attachedRigidbody.mass > 0.0000001f)
            {
                knifeObject = null;
                check = false;
                foreach (var honing in checkpoints)
                {
                    honing.check = false;
                }
            }
        }
    }
}
