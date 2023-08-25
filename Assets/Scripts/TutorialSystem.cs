using Autohand;
using Autohand.Demo;
using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
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
    private bool[] steps = new bool[12];
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
            if (!stepsVoiceOver[1])                                         // This is just so the sound does not keep playing on loop
            {
                voiceOvers[1].start();
                stepsVoiceOver[1] = true;
            }

            if (moveController.GetAxis2D(Common2DAxis.primaryAxis).x > 0.5f) hasMovement[0] = true;     // Right move
            if (moveController.GetAxis2D(Common2DAxis.primaryAxis).x < -0.5f) hasMovement[1] = true;    // Left move
            if (moveController.GetAxis2D(Common2DAxis.primaryAxis).y > 0.5f) hasMovement[2] = true;     // Up move
            if (moveController.GetAxis2D(Common2DAxis.primaryAxis).y < -0.5f) hasMovement[3] = true;    // Down move

            int tempCount = 0;
            foreach (var control in hasMovement) if (control) tempCount++;

            // If the player has moved from each axis then play complete particles and go to next step
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

            if (turnController.GetAxis2D(Common2DAxis.primaryAxis).x > 0.1f) hasRotation[0] = true;     // Right turn
            if (turnController.GetAxis2D(Common2DAxis.primaryAxis).x < -0.1f) hasRotation[1] = true;    // Left turn

            int tempCount = 0;
            foreach (var control in hasRotation) if (control) tempCount++;

            // If the player has rotated from each axis then play complete particles and go to next step
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

            // If item has been grabbed then play complete particles and go to next step
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

            // If both hands are fully washed
            if (rightHand.GetComponentInChildren<Dirty>().dirtiness <= 0.0f && leftHand.GetComponentInChildren<Dirty>().dirtiness <= 0.0f &&
            rightHand.GetComponentInChildren<Clean>().cleanness <= 0.0f && leftHand.GetComponentInChildren<Clean>().cleanness <= 0.0f &&
            rightHand.GetComponentInChildren<Wet>().wetness <= 0.0f && leftHand.GetComponentInChildren<Wet>().wetness <= 0.0f)
            {
                tempCheck = true;
            }

            var tempObject = FindAnyObjectByType(typeof(HandDryer));

            // If both hands are fully washed and hand dryer has stopped playing then play complete particle and go to next step
            if (tempCheck && tempObject.GetComponent<HandDryer>().airParticle.isStopped)
            {
                instructionIndex++;
                chapterIndex++;
                steps[3] = false;
                steps[4] = true;
            }

            // If users skipped steps and already cleaned then play complete particle and go to next step
            if (!rightHand.GetComponentInChildren<Dirty>() && !rightHand.GetComponentInChildren<Clean>() && !rightHand.GetComponentInChildren<Wet>() &&
                !leftHand.GetComponentInChildren<Dirty>() && !leftHand.GetComponentInChildren<Clean>() && !leftHand.GetComponentInChildren<Wet>())
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

            // If gloves are put on then play complete particle and go to next step
            if (!gloves.activeSelf)
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

            // If honer and knife have soap applied to them then play complete particle and go to next step
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

            // If honer and knife have water on them then play complete particle and go to next step
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

            // If honer and knife have been fully cleaned then play complete particle and go to next step
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

            // If knife has been fully straighten then play complete particle and go to next step
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

            // If meat has been put in the trash and destroyed then play complete particle and go to next step
            if (meat == null)
            {
                instructionIndex++;
                steps[9] = false;
                steps[10] = true;
            }
        }
        if (steps[10]) // Put on new gloves
        {
            if (!stepsVoiceOver[11])
            {
                voiceOvers[10].stop(STOP_MODE.IMMEDIATE);
                voiceOvers[11].start();
                stepsVoiceOver[11] = true;
            }

            if (!gloves.activeSelf)
            {
                instructionIndex++;
                steps[10] = false;
                steps[11] = true;
            }
        }
        if (steps[11]) // Cut meat
        {
            if (!stepsVoiceOver[12])
            {
                voiceOvers[11].stop(STOP_MODE.IMMEDIATE);
                voiceOvers[12].start();
                OverrideInstructionsDisplay();
                stepsVoiceOver[12] = true;
            }

            AnimatedInstructionsDisplay(cuttingImages);

            // If the meat has been cut then play complete particle and finish
            var cutMeat = FindObjectsOfType(typeof(CutMeat));
            if (cutMeat.Length > 0)
            {
                instructionIndex++;
                chapterIndex++;
                completeParticles[5].Play();
                OverrideInstructionsDisplay();
                steps[11] = false;
            }
        }
        if (chapterIndex == 4) // Finish
        {
            if (!stepsVoiceOver[13])
            {
                voiceOvers[12].stop(STOP_MODE.IMMEDIATE);
                voiceOvers[13].start();
                stepsVoiceOver[13] = true;
            }
        }
    }

    // Checks if item has been grabbed
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

    // Updates checkmarks
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

    // Updates instruction images
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

    // Disables all instruction images
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

    // Updates a GIF of an instruction
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
