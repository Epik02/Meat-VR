using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloveRespawn : MonoBehaviour
{
    public GameObject gloves;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<DropMeat>())
        {
            gloves.SetActive(true);
        }
    }
}
