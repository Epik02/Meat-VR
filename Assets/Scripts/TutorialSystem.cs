using Autohand;
using Autohand.Demo;
using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class TutorialSystem : MonoBehaviour
{
    public List<Image> honingImages;
    public List<Image> cuttingImages;
    public List<Image> instructions;
    public List<ParticleSystem> completeParticles;
    public Image[] checkmarks;
    public GameObject[] tools;
    public GameObject gloves;
    public GameObject player;
    public GameObject meat;

    private XRHandControllerLink moveController;
    private XRHandControllerLink turnController;
    private List<EventInstance> voiceOvers = new List<EventInstance>();
    private bool[] steps = new bool[11];
    private bool[] stepsVoiceOver = new bool[18];
    private GameObject honer;
    private GameObject knife;
    private int chapterIndex;
    private int instructionIndex;
    private float timer;
    private float lerpTime;
    private float lerpTravelTime = 0.25f;
    private int lerpIndex = 0;
    private bool isGrabbed = false;
    private bool startTutorial = false;
    private bool instructionsBool = true;
    private bool[] hasMovement = new bool[4];
    private bool[] hasRotation = new bool[2];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < FMODEvents.instance.tutorialVoiceOvers.Count; i++)
        {
            voiceOvers.Add(AudioManager.instance.CreateInstance(FMODEvents.instance.tutorialVoiceOvers[i]));
        }

        moveController = player.transform.parent.GetComponentsInChildren<XRHandControllerLink>()[1];
        turnController = player.transform.parent.GetComponentsInChildren<XRHandControllerLink>()[0];
        instructionIndex = 0;
        chapterIndex = -1;
        honer = tools[0];
        knife = tools[1];
    }

    // Update is called once per frame
    void Update()
    {
        CheckmarkDisplay();
        InstructionDisplay();

        timer += Time.deltaTime;

        if (!stepsVoiceOver[0])
        {
            voiceOvers[0].start();
            stepsVoiceOver[0] = true;
        }
        if (timer >= 6.5f && !startTutorial)
        {
            steps[0] = true;
            instructionIndex++;
            startTutorial = true;
        }

        if (steps[0]) // Movement
        {
            if (!stepsVoiceOver[1])
            {
                voiceOvers[1].start();
                stepsVoiceOver[1] = true;
            }

            if (moveController.GetAxis2D(Common2DAxis.primaryAxis).x > 0.5f) hasMovement[0] = true;   // Right Move
            if (moveController.GetAxis2D(Common2DAxis.primaryAxis).x < -0.5f) hasMovement[1] = true;   // Left Move
            if (moveController.GetAxis2D(Common2DAxis.primaryAxis).y > 0.5f) hasMovement[2] = true;   // Up Move
            if (moveController.GetAxis2D(Common2DAxis.primaryAxis).y < -0.5f) hasMovement[3] = true;   // Down Move

            int tempCount = 0;
            foreach (var control in hasMovement) if (control) tempCount++;
            if (tempCount == 4)
            {
                instructionIndex++;
                completeParticles[0].Play();
                steps[0] = false;
                steps[1] = true;
            }
        }
        if (steps[1]) // Rotation
        {
            if (!stepsVoiceOver[2])
            {
                voiceOvers[1].stop(STOP_MODE.IMMEDIATE);
                voiceOvers[2].start();
                stepsVoiceOver[2] = true;
            }

            if (turnController.GetAxis2D(Common2DAxis.primaryAxis).x > 0.1f) hasRotation[0] = true;   // Right Turn
            if (turnController.GetAxis2D(Common2DAxis.primaryAxis).x < -0.1f) hasRotation[1] = true;   // Left Turn

            int tempCount = 0;
            foreach (var control in hasRotation) if (control) tempCount++;
            if (tempCount == 2)
            {
                instructionIndex++;
                completeParticles[0].Play();
                steps[1] = false;
                steps[2] = true;
            }
        }
        if (steps[2]) // Pick up a tool
        {
            if (!stepsVoiceOver[3])
            {
                voiceOvers[2].stop(STOP_MODE.IMMEDIATE);
                voiceOvers[3].start();
                stepsVoiceOver[3] = true;
            }

            if (isGrabbed)
            {
                instructionIndex++;
                chapterIndex++;
                completeParticles[1].Play();
                steps[2] = false;
                steps[3] = true;
            }
        }
        if (steps[3]) // Wash hands
        {
            if (!stepsVoiceOver[4])
            {
                voiceOvers[3].stop(STOP_MODE.IMMEDIATE);
                voiceOvers[4].start();
                stepsVoiceOver[4] = true;
            }

            bool tempCheck = false;
            GameObject rightHand = gloves.GetComponent<Gloves>().rightHand;
            GameObject leftHand = gloves.GetComponent<Gloves>().leftHand;

            if (rightHand.GetComponentInChildren<Dirty>().dirtiness <= 0.0f && leftHand.GetComponentInChildren<Dirty>().dirtiness <= 0.0f &&
            rightHand.GetComponentInChildren<Clean>().cleanness <= 0.0f && leftHand.GetComponentInChildren<Clean>().cleanness <= 0.0f &&
            rightHand.GetComponentInChildren<Wet>().wetness <= 0.0f && leftHand.GetComponentInChildren<Wet>().wetness <= 0.0f)
            {
                tempCheck = true;
            }

            var tempObject = FindAnyObjectByType(typeof(HandDryer));

            if (tempCheck && tempObject.GetComponent<HandDryer>().airParticle.isStopped)
            {
                instructionIndex++;
                chapterIndex++;
                steps[3] = false;
                steps[4] = true;
            }
        }
        if (steps[4]) // Put gloves on
        {
            if (!stepsVoiceOver[5])
            {
                voiceOvers[4].stop(STOP_MODE.IMMEDIATE);
                voiceOvers[5].start();
                stepsVoiceOver[5] = true;
            }

            if (gloves == null)
            {
                instructionIndex++;
                completeParticles[2].Play();
                steps[4] = false;
                steps[5] = true;
            }
        }
        if (steps[5]) // Soap tools
        {
            if (!stepsVoiceOver[6])
            {
                voiceOvers[5].stop(STOP_MODE.IMMEDIATE);
                voiceOvers[6].start();
                stepsVoiceOver[6] = true;
            }

            if (honer.GetComponent<Dirty>().dirtiness <= 0 && honer.GetComponent<Clean>().cleanness >= 100 &&
                knife.GetComponentInChildren<Dirty>().dirtiness <= 0 && knife.GetComponentInChildren<Clean>().cleanness >= 100)
            {
                instructionIndex++;
                steps[5] = false;
                steps[6] = true;
            }
        }
        if (steps[6]) // Clean tools
        {
            if (!stepsVoiceOver[7])
            {
                voiceOvers[6].stop(STOP_MODE.IMMEDIATE);
                voiceOvers[7].start();
                stepsVoiceOver[7] = true;
            }

            if (honer.GetComponent<Clean>().cleanness <= 0 && honer.GetComponent<Wet>().wetness >= 100 &&
                knife.GetComponentInChildren<Clean>().cleanness <= 0 && knife.GetComponentInChildren<Wet>().wetness >= 100)
            {
                instructionIndex++;
                steps[6] = false;
                steps[7] = true;
            }
        }
        if (steps[7]) // Sanitize tools
        {
            if (!stepsVoiceOver[8])
            {
                voiceOvers[7].stop(STOP_MODE.IMMEDIATE);
                voiceOvers[8].start();
                stepsVoiceOver[8] = true;
            }

            if (honer.GetComponent<Dirty>().dirtiness <= 0 && honer.GetComponent<Clean>().cleanness <= 0 && honer.GetComponent<Wet>().wetness <= 0 &&
                knife.GetComponentInChildren<Dirty>().dirtiness <= 0 && knife.GetComponentInChildren<Clean>().cleanness <= 0 && knife.GetComponentInChildren<Wet>().wetness <= 0)
            {
                instructionIndex++;
                chapterIndex++;
                completeParticles[3].Play();
                steps[7] = false;
                steps[8] = true;
            }
        }
        if (steps[8]) // Straighten knife
        {
            if (!stepsVoiceOver[9])
            {
                voiceOvers[8].stop(STOP_MODE.IMMEDIATE);
                voiceOvers[9].start();
                OverrideInstructionsDisplay();
                stepsVoiceOver[9] = true;
            }

            AnimatedInstructionsDisplay(honingImages);

            if (knife.GetComponent<KnifeStraighten>().strength >= 100.0f)
            {
                instructionIndex++;
                chapterIndex++;
                completeParticles[4].Play();
                OverrideInstructionsDisplay();
                steps[8] = false;
                steps[9] = true;
            }
        }
        if (steps[9]) // Throw out meat
        {
            if (!stepsVoiceOver[10])
            {
                voiceOvers[9].stop(STOP_MODE.IMMEDIATE);
                voiceOvers[10].start();
                meat.GetComponent<DropMeat>().enabled = true;
                stepsVoiceOver[10] = true;
            }

            if (meat == null)
            {
                instructionIndex++;
                steps[9] = false;
                steps[10] = true;
            }
        }
        if (steps[10]) // Cut meat
        {
            if (!stepsVoiceOver[11])
            {
                voiceOvers[10].stop(STOP_MODE.IMMEDIATE);
                voiceOvers[11].start();
                OverrideInstructionsDisplay();
                stepsVoiceOver[11] = true;
            }

            AnimatedInstructionsDisplay(cuttingImages);

            var cutMeat = FindObjectsOfType(typeof(CutMeat));
            if (cutMeat.Length > 0)
            {
                instructionIndex++;
                chapterIndex++;
                completeParticles[5].Play();
                OverrideInstructionsDisplay();
                steps[10] = false;
            }
        }
        if (chapterIndex == 4) // Finish
        {
            if (!stepsVoiceOver[12])
            {
                voiceOvers[11].stop(STOP_MODE.IMMEDIATE);
                voiceOvers[12].start();
                stepsVoiceOver[12] = true;
            }
        }
    }

    public void BeingGrabbed(GameObject tool)
    {
        foreach (var item in tools)
        {
            if (item == tool)
            {
                completeParticles[1].gameObject.transform.position = tool.transform.position;
                isGrabbed = true;
            }
        }
    }

    public void CheckmarkDisplay()
    {
        for (int i = 0; i < checkmarks.Length; i++)
        {
            if (i <= chapterIndex)
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
        if (instructionsBool)
        {
            for (int i = 0; i < instructions.Count; i++)
            {
                if (i == instructionIndex)
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

    public void OverrideInstructionsDisplay()
    {
        instructionsBool = !instructionsBool;
        lerpTime = 0.0f;
        lerpIndex = 0;

        foreach (var item in instructions)
        {
            item.enabled = false;
        }
        foreach (var item in honingImages)
        {
            item.enabled = false;
        }
        foreach (var item in cuttingImages)
        {
            item.enabled = false;
        }
    }

    public void AnimatedInstructionsDisplay(List<Image> images)
    {
        lerpTime += Time.deltaTime;

        for (int i = 0; i < images.Count; i++)
        {
            if (images[i] == images[lerpIndex])
            {
                images[i].enabled = true;
            }
            else
            {
                images[i].enabled = false;
            }
        }

        if (lerpTime > lerpTravelTime)
        {
            lerpTime = 0.0f;
            lerpIndex++;

            if (lerpIndex >= images.Count)
            {
                lerpIndex = 0;
            }
        }
    }
}
