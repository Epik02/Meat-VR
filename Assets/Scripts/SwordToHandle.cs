using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordToHandle : MonoBehaviour
{
    public GameObject swordHandle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.SetPositionAndRotation(swordHandle.transform.position, this.transform.rotation);
    }
}
