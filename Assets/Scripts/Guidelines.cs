using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Guidelines : MonoBehaviour
{
    List<CuttingSurface.KnifeAccuracy> triggers = new List<CuttingSurface.KnifeAccuracy>();

    public GameObject nextStep;
    public Transform cuttablePlane;
    public Renderer lineColour;
    public LineController line;
    public TMP_Text accuracyText;
    public bool hasStartedCutting { get; private set; } = false;

    private GameObject knife;

    // Start is called before the first frame update
    private void Start()
    {
        knife = GameObject.Find("AccuracyTestKnife");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "AccuracyTestKnife") 
        {
            hasStartedCutting = true;
            lineColour.material.color = Color.green;
            lineColour.material.SetColor("_EmissionColor", Color.green);
        }
    }
    public void OnKnifeEnter(CuttingSurface.KnifeAccuracy acc) 
    {
        triggers.Add(acc);
        UpdateLook();
    }
    public void OnKnifeExit(CuttingSurface.KnifeAccuracy acc) 
    {
        triggers.Remove(acc);
        UpdateLook();
    }

    void NextStep()
    {
        if (nextStep != null)
        {
            nextStep.SetActive(true);
            line.guide = nextStep.GetComponentInChildren<Guidelines>();
            return;
        }
    }

    void UpdateLook()
    {
        Slice sliceObject = knife.GetComponent<Slice>();
        Collider[] objectsToBeSliced = Physics.OverlapBox(knife.transform.position, new Vector3(1, 0.1f, 0.1f), knife.transform.rotation, sliceObject.sliceableLayer);

        if (!hasStartedCutting)
        {
            return;
        }

        if (knife.GetComponent<KnifeStraighten>().strength >= 100.0f)
        {
            if (triggers.IndexOf(CuttingSurface.KnifeAccuracy.BAD) != -1)
            {
                if (triggers.IndexOf(CuttingSurface.KnifeAccuracy.COMPLETE) != -1)
                {
                    // Complete
                    if (line.NextGuideline())
                    {
                        foreach (var item in objectsToBeSliced)
                        {
                            if (item != null)
                            {
                                float overallAccuracy = item.GetComponent<AccuracySystem>().CalculateAccuracy();
                                ScoreManager.instance.AddScore(Mathf.RoundToInt(overallAccuracy));
                                accuracyText.text = "Accuracy: " + overallAccuracy.ToString("F2") + "%";
                                if (nextStep != null)
                                {
                                    sliceObject.SlicePlane(item.transform.gameObject, cuttablePlane, true);
                                }
                                else
                                {
                                    sliceObject.SlicePlane(item.transform.gameObject, cuttablePlane, false);
                                }
                            }
                        }
                        NextStep();
                        lineColour.material.color = Color.white;
                        lineColour.material.SetColor("_EmissionColor", Color.white);
                        hasStartedCutting = false;
                        line.GenerateHitboxes();
                        Destroy(transform.parent.gameObject);
                        return;
                    }
                }
                if (triggers.IndexOf(CuttingSurface.KnifeAccuracy.GOOD) != -1)
                {
                    // Highest accuracy
                    lineColour.material.color = Color.green;
                    lineColour.material.SetColor("_EmissionColor", Color.green);
                    return;
                }
                // Lowest accuracy
                lineColour.material.color = Color.red;
                lineColour.material.SetColor("_EmissionColor", Color.red);
                hasStartedCutting = false;
                return;
            }
        }
    }
}
