using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Guidelines : MonoBehaviour
{
    List<CuttingSurface.KnifeAccuracy> triggers = new List<CuttingSurface.KnifeAccuracy>();

    public GameObject item;
    public GameObject nextStep;
    public Transform cuttablePlane;
    public Renderer lineColour;
    public LineController line;
    public TMP_Text accuracyText;
    public bool hasStartedCutting { get; private set; } = false;
    public bool tutorial = false;

    private bool canCut = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == item.name)    // If the slice object has entered then start cutting
        {
            hasStartedCutting = true;
            lineColour.material.color = Color.green;
            lineColour.material.SetColor("_EmissionColor", Color.green);
        }
    }

    // Will add knife accuracy triggers when slice object has entered
    public void OnKnifeEnter(CuttingSurface.KnifeAccuracy acc) 
    {
        triggers.Add(acc);
        UpdateLook();
    }

    // Will remove knife accuracy triggers when slice object has exited
    public void OnKnifeExit(CuttingSurface.KnifeAccuracy acc) 
    {
        triggers.Remove(acc);
        UpdateLook();
    }

    // Will set up the next step in the cutting stages
    void NextStep()
    {
        if (nextStep != null)
        {
            nextStep.SetActive(true);
            line.guide = nextStep.GetComponentInChildren<Guidelines>();
            return;
        }
    }

    // Will update the look of the guideline and slice the meat
    void UpdateLook()
    {
        // Gets the slice object and checks what objects are colliding with the slice object
        Slice sliceObject = item.GetComponent<Slice>();
        Collider[] objectsToBeSliced = Physics.OverlapBox(item.transform.position, new Vector3(1, 0.1f, 0.1f), item.transform.rotation, sliceObject.sliceableLayer);

        if (!hasStartedCutting)                                             // If not started cutting then restart
        {
            return;
        }

        if (item.GetComponent<KnifeStraighten>())
        {
            if (item.GetComponent<KnifeStraighten>().strength >= 100.0f)    // If knife is at 100% strength then can cut
            {
                canCut = true;
            }
        }
        else                                                                // If knife straigthen does not exist then can cut
        {
            canCut = true;
        }

        if (canCut)                                                         // If can cut is true...
        {
            if (triggers.IndexOf(CuttingSurface.KnifeAccuracy.BAD) != -1)
            {
                if (triggers.IndexOf(CuttingSurface.KnifeAccuracy.COMPLETE) != -1)
                {
                    if (tutorial)                                           // If this is a tutorial then do a basic cut
                    {
                        foreach (var item in objectsToBeSliced)
                        {
                            if (item != null)
                            {
                                float overallAccuracy = item.GetComponent<AccuracySystem>().CalculateAccuracy();
                                accuracyText.text = "Accuracy: " + overallAccuracy.ToString("F2") + "%";
                                sliceObject.SliceBasic(item.transform.gameObject, cuttablePlane);
                            }
                        }
                        lineColour.material.color = Color.white;
                        lineColour.material.SetColor("_EmissionColor", Color.white);
                        hasStartedCutting = false;
                        line.GenerateHitboxes();
                        return;
                    }

                    // Complete
                    // If there is a next guideline and this is not a tutorial...
                    if (line.NextGuideline() && !tutorial)
                    {
                        foreach (var item in objectsToBeSliced)
                        {
                            if (item != null)
                            {
                                // If there is an accuracy system then add the accuracy to the score manager
                                float overallAccuracy = item.GetComponent<AccuracySystem>().CalculateAccuracy();
                                if (ScoreManager.instance)
                                {
                                    ScoreManager.instance.AddScore(Mathf.RoundToInt(overallAccuracy));
                                }
                                // Update the text of the accuracy
                                accuracyText.text = "Accuracy: " + overallAccuracy.ToString("F2") + "%";
                                // If next step exists then do a slice of the meat and freeze the meat position for the next step
                                if (nextStep != null)
                                {
                                    sliceObject.SlicePlane(item.transform.gameObject, cuttablePlane, true);
                                }
                                // Otherwise slice the meat and do not freeze the meat position
                                else
                                {
                                    sliceObject.SlicePlane(item.transform.gameObject, cuttablePlane, false);
                                }
                            }
                        }
                        // Reset the lines for the next step and destroy the game object of the current step
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
