using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccuracyLocation : MonoBehaviour
{

    public GameObject meat1;
    public GameObject AccuracyObject;
    public GameObject completed;
    public GameObject wineffect;

    public Vector3 location2 = new Vector3();
    private int cutcount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (meat1 == null && cutcount < 1)
        {
            cutcount = cutcount + 1;
            AccuracyObject.transform.position = location2;
        }

        if (GameObject.Find("Upper_Hull") != null){
            cutcount = 2;
        }

        if (cutcount == 2)
        {
            AccuracyObject.SetActive(false);
            wineffect.SetActive(true);
            completed.SetActive(true);
        }
    }
}
