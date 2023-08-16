using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Autohand;
using Unity.VisualScripting;
using FMOD.Studio;

public class ObjectiveTracker : MonoBehaviour
{
    public GameObject rightHand;
    public GameObject leftHand;
    public GameObject gloves;
    public GameObject knife;
    public GameObject meat;
    public Image[] checkmarks;
    public ParticleSystem[] completeParticles;
    public Image[] instructions;

    private bool[] steps = new bool[6];
    private bool[] stepsVoiceOver = new bool[18];
    private int index;
    private List<EventInstance> voiceOvers = new List<EventInstance>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < FMODEvents.instance.gameVoiceOvers.Count; i++)
        {
            voiceOvers.Add(AudioManager.instance.CreateInstance(FMODEvents.instance.gameVoiceOvers[i]));
        }

        if (rightHand == null && leftHand == null)
        {
            index = 0;
            steps[1] = true;
        }
        else
        {
            index = -1;
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
        InstructionDisplay();
        CheckmarkDisplay();

        if (steps[0]) // Wash hands
        {
            //if (!stepsVoiceOver[0])
            //{
            //    voiceOvers[0].start();
            //    stepsVoiceOver[0] = true;
            //}

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
                ScoreManager.instance.AddScore(10);
                steps[0] = false;
                steps[1] = true;
                index++;
            }
        }
        if (steps[1]) // Put on gloves
        {
            //if (!stepsVoiceOver[1])
            //{
            //    voiceOvers[0].stop(STOP_MODE.IMMEDIATE);
            //    voiceOvers[1].start();
            //    stepsVoiceOver[1] = true;
            //}

            var grabbables = FindObjectsOfType(typeof(Grabbable));

            if (gloves == null)
            {
                foreach (var item in grabbables)
                {
                    item.GetComponent<Grabbable>().enabled = true;
                }

                completeParticles[1].Play();
                ScoreManager.instance.AddScore(5);
                steps[1] = false;
                steps[2] = true;
                index++;
            }
        }
        if (steps[2]) // Straighten knife
        {
            //if (!stepsVoiceOver[2])
            //{
            //    voiceOvers[1].stop(STOP_MODE.IMMEDIATE);
            //    voiceOvers[2].start();
            //    stepsVoiceOver[2] = true;
            //}

            if (knife.GetComponent<KnifeStraighten>().strength >= 100.0f)
            {
                completeParticles[2].Play();
                steps[2] = false;
                steps[3] = true;
                index++;
            }
        }
        if (steps[3]) // Place meat
        {
            //if (!stepsVoiceOver[3])
            //{
            //    voiceOvers[2].stop(STOP_MODE.IMMEDIATE);
            //    voiceOvers[3].start();
            //    stepsVoiceOver[3] = true;
            //}

            if (meat.GetComponent<Collider>().isTrigger)
            {
                completeParticles[3].Play();
                steps[3] = false;
                steps[4] = true;
                index = 4;
            }
        }
        if (steps[4]) // Cut meat
        {
            //if (!stepsVoiceOver[4])
            //{
            //    voiceOvers[3].stop(STOP_MODE.IMMEDIATE);
            //    voiceOvers[4].start();
            //    stepsVoiceOver[4] = true;
            //}

            if (meat == null)
            {
                completeParticles[4].Play();
                steps[4] = false;
                steps[5] = true;
                index++;
            }
        }
        if (steps[5]) // Finish
        {
            //if (!stepsVoiceOver[5])
            //{
            //    voiceOvers[4].stop(STOP_MODE.IMMEDIATE);
            //    voiceOvers[5].start();
            //    stepsVoiceOver[5] = true;
            //}

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

    public void InstructionDisplay()
    {
        for (int i = 0; i < instructions.Length; i++)
        {
            if (i - 1 == index)
            {
                instructions[i].enabled = true;
            }
            else
            {
                instructions[i].enabled = false;
            }
        }
    }
}
