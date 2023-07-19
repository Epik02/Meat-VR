using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionFollower : MonoBehaviour
{
    public Transform cameraTransform;
    public float distance = 3.0f;

    private bool isCentered = false;

    private void OnBecameInvisible()
    {
        isCentered = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCentered)
        {
            Vector3 targetPosition = FindTargetPosition();
            MoveTowards(targetPosition);
            if (ReachedPosition(targetPosition))
            {
                isCentered = true;
            }
        }
    }

    private Vector3 FindTargetPosition()
    {
        return cameraTransform.position + (cameraTransform.forward * distance);
    }

    private void MoveTowards(Vector3 targetPosition)
    {
        transform.position += (targetPosition - transform.position) * 0.025f;
    }

    private bool ReachedPosition(Vector3 targetPosition)
    {
        return Vector3.Distance(targetPosition, cameraTransform.position) < 0.1f;
    }
}
