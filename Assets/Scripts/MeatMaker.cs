using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatMaker : MonoBehaviour
{
    public GameObject meatPrefab;
    public Transform spawnPosition;

    private GameObject newMeat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateMeat()
    {
        var meats = GameObject.FindGameObjectsWithTag("Meat");
        foreach (var meat in meats) 
        {
            Destroy(meat);
        }
        newMeat = Instantiate(meatPrefab);
        newMeat.transform.SetPositionAndRotation(spawnPosition.position, spawnPosition.rotation);
    }
}
