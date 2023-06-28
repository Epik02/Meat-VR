using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.Threading;
using TMPro;

public class Accuracy : MonoBehaviour
{
    public Stopwatch timer = new Stopwatch();
    private float elapsedFrames;
    public float speed;
    public float timeBetweenInstantiation;
    bool inMeatInGeneral = false;
    public float myTimer;
    double totalTime;
    Stopwatch intervalTimer = new Stopwatch();
    GameObject instantiatedObject;
    public GameObject objectToInstantiate;
    public GameObject Knife;
    private List<GameObject> knifePos = new List<GameObject>();
    private List<bool> knifePath = new List<bool>();

    public CuttingPathCheck cuttingPathCheck;

    private int listNum = 0;
    public GameObject accuracyObject;
    private float currentAccuracy;
    private float overallAccuracy;
    public TextMeshPro mText;
    public TextMeshPro mText2;
    private float hit;
    private float missed;
    private float acctest;
    public GameObject accobjA;
    public GameObject accobjB;
    public GameObject accobjC;
    public GameObject accobjD;
    public GameObject accobjE;
    public GameObject accobjF;
    public GameObject accobjG;

    // Start is called before the first frame update
    void Start()
    {
        cuttingPathCheck = accuracyObject.GetComponent<CuttingPathCheck>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "AccuracyTestKnife")
        {
            inMeatInGeneral = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "AccuracyTestKnife")
        {
            UnityEngine.Debug.Log("exitedtrigger");
            inMeatInGeneral = false;
            AddAccuracy();
            mText.text = "Accuracy: " + overallAccuracy + "%";

            listNum = 0;
            //resets list so a new accuracy is determined each cut
            knifePos.Clear();
            knifePath.Clear();
            //UnityEngine.Debug.Log(knifeAccuracy.Count);
        }
    }

    private void AddAccuracy()
    {
        UnityEngine.Debug.Log("ADDING ACCURACY");

        float correctHits = 0;
        float allHits = 0;

        for (int i = 0; i < listNum; ++i)
        {
            if (knifePath[i] == true)
            {
                correctHits += 1;
            }
            allHits += 1;
        }

        UnityEngine.Debug.Log("CORRECT HITS: " + correctHits.ToString());
        UnityEngine.Debug.Log("TOTAL HITS: " + allHits.ToString());

        overallAccuracy = (correctHits / allHits) * 100;

        UnityEngine.Debug.Log("OVERALL ACC: " + overallAccuracy.ToString());

        UnityEngine.Debug.Log("END ADD ACCURACY");

    }

    // Update is called once per frame
    void Update()
    {
        //mText2.text = "IN MEAT? " + inMeatInGeneral.ToString() + ". IN CUT PATH? " + cuttingPathCheck.cuttingInPath.ToString();

        if (inMeatInGeneral)
        {
            

            //elapsedFrames += Time.deltaTime * speed;
            timer.Start();
            intervalTimer.Start();
            if (intervalTimer.Elapsed.TotalSeconds >= timeBetweenInstantiation)
            {
                instantiatedObject = Instantiate(objectToInstantiate);
                instantiatedObject.transform.SetPositionAndRotation(Knife.transform.position, Knife.transform.rotation); // instantiates an object every (timeBetweenInstantiation) seconds so we know where the knife was
                knifePos.Add(instantiatedObject.transform.gameObject);
                if (cuttingPathCheck.cuttingInPath == true)
                {
                    knifePath.Add(true);
                }
                else
                {
                    knifePath.Add(false);
                }
                //UnityEngine.Debug.Log(knifePos[listNum]);
                listNum = listNum + 1;
                Destroy(instantiatedObject, 3f);
                intervalTimer.Restart();
                //UnityEngine.Debug.Log("Time between instantiation: " + timeBetweenInstantiation);
            }

            totalTime = intervalTimer.Elapsed.TotalSeconds;
            //UnityEngine.Debug.Log("Total Time: " + totalTime);

        }
        else
        {
            timer.Stop();
            timer.Reset();
            //AddAccuracy();
        }
    }
}
