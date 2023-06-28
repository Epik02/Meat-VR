using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoningCheckpoint : MonoBehaviour
{
    public LayerMask knifeLayer;
    public bool check = false;

    private void OnTriggerEnter(Collider other)
    {
        if (knifeLayer == (knifeLayer | (1 << other.gameObject.layer)))
        {
            if (other.attachedRigidbody == null || other.attachedRigidbody.mass > 0.0000001f)
            {
                check = true;
            }
        }
    }
}
