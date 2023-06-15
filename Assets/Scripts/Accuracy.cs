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

    //Thread loopThread = new Thread(() => intervalInstantiate());


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

    //private void newAccuracy()
    //{
    //    hit = PlayerPrefs.GetInt("AccuracyObjectHit");
    //    missed = PlayerPrefs.GetInt("AccuracyObjectMissed");
    //    UnityEngine.Debug.Log("hit: " + hit);
    //    UnityEngine.Debug.Log("missed: " + missed);
    //    acctest = hit / (hit + missed);

    //}

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

    //private void AddAccuracy()
    //{
    //    if (listNum > 0)
    //    {
    //        //adds up accuracy of instantiated objects compared to accuracy object z position (will change this to check xyz positions of child objects later for more accurate percentage)
    //        for (int i = 0; i < listNum; i++) //checks to see how close the knifes positions were to the accuracy object(s)
    //        {
    //            if (knifePos[i].z > accuracyObject.transform.position.z)
    //            {
    //                currentAccuracy = Mathf.Abs(accuracyObject.transform.position.z) / Mathf.Abs(knifePos[i].z); //problem here
    //                knifeAccuracy.Add(currentAccuracy);
    //                //UnityEngine.Debug.Log("knifepos: " + knifePos[i].z);
    //                //UnityEngine.Debug.Log("accuracyobj: " + accuracyObject.transform.position.z);
    //            }
    //            else
    //            {
    //                currentAccuracy = Mathf.Abs(knifePos[i].z) / Mathf.Abs(accuracyObject.transform.position.z);
    //                knifeAccuracy.Add(currentAccuracy);
    //                //UnityEngine.Debug.Log("knifepos: " + knifePos[i].z);
    //                //UnityEngine.Debug.Log("accuracyobj: " + accuracyObject.transform.position.z);
    //            }
    //        }

    //        //adds up overall accuracy percentage
    //        for (int i = 0; i < listNum; i++)
    //        {
    //            overallAccuracy = overallAccuracy + knifeAccuracy[i];
    //            if (i == (listNum - 1))
    //            {
    //                overallAccuracy = overallAccuracy / (listNum + 1);
    //                //overallAccuracy = overallAccuracy * 100;
    //                UnityEngine.Debug.Log("we hittin this code");
    //                //accuracy displayed on text in ontriggerexit
    //            }
    //        }
    //    }
    //}

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
