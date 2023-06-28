using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class RespawnKnife : MonoBehaviour
{
    public Stopwatch timer = new Stopwatch();
    public GameObject knife;
    public float respawnYPos;
    public float timeToRespawn;
    public Vector3 newKnifePos = new Vector3();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (knife.transform.position.y < -0.6)
        {
            timer.Start();
            if (timer.Elapsed.TotalSeconds > timeToRespawn)
            {
                knife.transform.SetPositionAndRotation(newKnifePos, new Quaternion(0, 0, 0, 180));
            }
        }

        if (knife.transform.position == newKnifePos)
        {
            timer.Stop();
            timer.Reset();
        }
    }
}
