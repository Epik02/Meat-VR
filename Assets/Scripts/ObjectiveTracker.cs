using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Autohand;
using Unity.VisualScripting;

public class ObjectiveTracker : MonoBehaviour
{
    public GameObject rightHand;
    public GameObject leftHand;
    public GameObject gloves;
    public GameObject knife;
    public GameObject meat;
    public Image[] checkmarks;
    public TMP_Text stepText;
    public ParticleSystem[] completeParticles;

    private bool[] steps = new bool[6] { false, false, false, false, false, false };
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        if (rightHand == null && leftHand == null)
        {
            steps[1] = true;
        }
        else
        {
            steps[0] = true;
        }
        foreach (var particle in completeParticles) 
        {
            particle.Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckmarkDisplay();

        if (steps[0])
        {
            stepText.text = "Step 1: Wash your hands";

            var grabbables = FindObjectsOfType(typeof(Grabbable));
            foreach (var item in grabbables)
            {
                item.GetComponent<Grabbable>().enabled = false;
            }

            if (rightHand.GetComponentInChildren<Dirty>().dirtiness <= 0.0f && leftHand.GetComponentInChildren<Dirty>().dirtiness <= 0.0f &&
                rightHand.GetComponentInChildren<Clean>().cleanness <= 0.0f && leftHand.GetComponentInChildren<Clean>().cleanness <= 0.0f &&
                rightHand.GetComponentInChildren<Wet>().wetness <= 0.0f && leftHand.GetComponentInChildren<Wet>().wetness <= 0.0f)
            {
                completeParticles[0].Play();
                steps[0] = false;
                steps[1] = true;
                index++;
            }
        }
        if (steps[1])
        {
            stepText.text = "Step 2: Put on gloves";

            var grabbables = FindObjectsOfType(typeof(Grabbable));

            if (gloves == null)
            {
                foreach (var item in grabbables)
                {
                    item.GetComponent<Grabbable>().enabled = true;
                }

                completeParticles[1].Play();
                Destroy(rightHand.GetComponentInChildren<Dirty>());
                Destroy(rightHand.GetComponentInChildren<Clean>());
                Destroy(rightHand.GetComponentInChildren<Wet>());
                Destroy(leftHand.GetComponentInChildren<Dirty>());
                Destroy(leftHand.GetComponentInChildren<Clean>());
                Destroy(leftHand.GetComponentInChildren<Wet>());
                steps[1] = false;
                steps[2] = true;
                index++;
            }
        }
        if (steps[2])
        {
            stepText.text = "Step 3: Straighten your knife";
            if (knife.GetComponent<KnifeStraighten>().strength >= 100.0f)
            {
                completeParticles[2].Play();
                steps[2] = false;
                steps[3] = true;
                index++;
            }
        }
        if (steps[3])
        {
            stepText.text = "Step 4: Bring the meat to the green indicator";
            if (meat.GetComponent<Collider>().isTrigger)
            {
                completeParticles[3].Play();
                steps[3] = false;
                steps[4] = true;
                index++;
            }
        }
        if (steps[4])
        {
            stepText.text = "Step 5: Cut the meat by following the guidelines";
            if (meat == null)
            {
                completeParticles[4].Play();
                steps[4] = false;
                steps[5] = true;
                index++;
            }
        }
        if (steps[5])
        {
            stepText.text = "Objectives Complete!";
            steps[5] = false;
        }
    }

    public void CheckmarkDisplay()
    {
        for (int i = 0; i < checkmarks.Length; i++)
        {
            if (i <= index)
            {
                checkmarks[i].enabled = true;
            }
            else
            {
                checkmarks[i].enabled = false;
            }
        }
    }
}
