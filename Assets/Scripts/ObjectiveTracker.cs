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
    public GameObject knife;
    public GameObject meat;
    public Image[] checkmarks;
    public TMP_Text stepText;
    public ParticleSystem[] completeParticles;

    private bool[] steps = new bool[5] { false, false, false, false, false };
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        steps[0] = true;
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

            if (rightHand.GetComponent<Dirty>().dirtiness <= 0.0f && leftHand.GetComponent<Dirty>().dirtiness <= 0.0f)
            {
                foreach (var item in grabbables)
                {
                    item.GetComponent<Grabbable>().enabled = true;
                }

                completeParticles[0].Play();
                steps[0] = false;
                steps[1] = true;
                index++;
            }
        }
        if (steps[1])
        {
            stepText.text = "Step 2: Strengthen your knife";
            if (knife.GetComponent<KnifeStrength>().strength >= 100.0f)
            {
                completeParticles[1].Play();
                steps[1] = false;
                steps[2] = true;
                index++;
            }
        }
        if (steps[2])
        {
            stepText.text = "Step 3: Bring the meat to the green indicator";
            if (meat.GetComponent<Collider>().isTrigger)
            {
                completeParticles[2].Play();
                steps[2] = false;
                steps[3] = true;
                index++;
            }
        }
        if (steps[3])
        {
            stepText.text = "Step 4: Cut the meat by following the guidelines";
            if (meat == null)
            {
                completeParticles[3].Play();
                steps[3] = false;
                steps[4] = true;
                index++;
            }
        }
        if (steps[4])
        {
            stepText.text = "Objectives Complete!";
            //completeParticles[4].Play();
            steps[4] = false;
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
