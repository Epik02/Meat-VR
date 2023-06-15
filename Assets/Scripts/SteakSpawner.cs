using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteakSpawner : MonoBehaviour
{
    bool intrigger = false;
    public GameObject Steak;
    GameObject newSteak;
    public Vector3 pos = new Vector3();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "RobotHand (R)")
        {
            intrigger = true;
            Debug.Log("TESTETSTES");
            newSteak = Instantiate(Steak);
            newSteak.transform.SetPositionAndRotation(pos, transform.rotation);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "RobotHand (R)")
        {
            intrigger = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
