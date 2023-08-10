using Autohand;
using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialSystem : MonoBehaviour
{
    public List<Image> instructions;
    public List<ParticleSystem> completeParticles;
    public Image[] checkmarks;
    //public TMP_Text stepText;
    public GameObject[] tools;
    public GameObject gloves;
    public GameObject player;

    private List<EventInstance> voiceOvers = new List<EventInstance>();
    private bool[] steps = new bool[8];
    private bool[] stepsVoiceOver = new bool[15];
    private GameObject honer;
    private GameObject knife;
    private int chapterIndex;
    private int instructionIndex;
    private float timer;
    private bool isGrabbed = false;
    private bool startTutorial = false;
    private Vector3 playerOriginalPosition;
    private Quaternion playerOriginalRotation;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < FMODEvents.instance.tutorialVoiceOvers.Count; i++)
        {
            voiceOvers.Add(AudioManager.instance.CreateInstance(FMODEvents.instance.tutorialVoiceOvers[i]));
        }

        instructionIndex = 0;
        chapterIndex = -1;
        //steps[0] = true;
        honer = tools[0];
        knife = tools[1];
        playerOriginalPosition = player.transform.position;
        playerOriginalRotation = player.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        CheckmarkDisplay();
        InstructionDisplay();

        timer += Time.deltaTime;

        if (!stepsVoiceOver[0])
        {
            //AudioManager.instance.PlayOneShot(FMODEvents.instance.tutorialVoiceOvers[0], player.transform.position);
            voiceOvers[0].start();
            stepsVoiceOver[0] = true;
        }
        if (timer >= 6.5f && !startTutorial)
        {
            steps[0] = true;
            instructionIndex++;
            startTutorial = true;
        }

        if (steps[0])
        {
            //stepText.text = "Move and Rotate";

            if (!stepsVoiceOver[1])
            {
                //AudioManager.instance.PlayOneShot(FMODEvents.instance.tutorialVoiceOvers[1], player.transform.position);
                voiceOvers[1].start();
                stepsVoiceOver[1] = true;
            }

            float distance = Vector3.Distance(player.transform.position, playerOriginalPosition);
            if (distance > 1.0f && player.transform.rotation != playerOriginalRotation)
            {
                instructionIndex++;
                //index++;
                completeParticles[0].Play();
                steps[0] = false;
                steps[1] = true;
            }
        }
        if (steps[1])
        {
            //stepText.text = "Pick Up Tools";

            if (!stepsVoiceOver[2])
            {
                //AudioManager.instance.PlayOneShot(FMODEvents.instance.tutorialVoiceOvers[3], player.transform.position);
                voiceOvers[1].stop(STOP_MODE.IMMEDIATE);
                voiceOvers[3].start();
                stepsVoiceOver[2] = true;
            }

            if (isGrabbed)
            {
                instructionIndex++;
                chapterIndex++;
                completeParticles[1].Play();
                steps[1] = false;
                steps[2] = true;
            }
        }
        if (steps[2])
        {
            if (!stepsVoiceOver[3])
            {
                //AudioManager.instance.PlayOneShot(FMODEvents.instance.tutorialVoiceOvers[4], player.transform.position);
                voiceOvers[3].stop(STOP_MODE.IMMEDIATE);
                voiceOvers[4].start();
                stepsVoiceOver[3] = true;
            }

            GameObject rightHand = gloves.GetComponent<Gloves>().rightHand;
            GameObject leftHand = gloves.GetComponent<Gloves>().leftHand;
            if (rightHand.GetComponentInChildren<Dirty>().dirtiness <= 0.0f && leftHand.GetComponentInChildren<Dirty>().dirtiness <= 0.0f &&
            rightHand.GetComponentInChildren<Clean>().cleanness <= 0.0f && leftHand.GetComponentInChildren<Clean>().cleanness <= 0.0f &&
            rightHand.GetComponentInChildren<Wet>().wetness <= 0.0f && leftHand.GetComponentInChildren<Wet>().wetness <= 0.0f)
            {
                instructionIndex++;
                chapterIndex++;
                steps[2] = false;
                steps[3] = true;
            }
        }
        if (steps[3])
        {
            //stepText.text = "Wash Hands and Wear Gloves";

            if (!stepsVoiceOver[4])
            {
                //AudioManager.instance.PlayOneShot(FMODEvents.instance.tutorialVoiceOvers[5], player.transform.position);
                voiceOvers[4].stop(STOP_MODE.IMMEDIATE);
                voiceOvers[5].start();
                stepsVoiceOver[4] = true;
            }

            if (gloves == null)
            {
                instructionIndex++;
                //index++;
                completeParticles[2].Play();
                steps[3] = false;
                steps[4] = true;
            }
        }
        if (steps[4])
        {
            if (!stepsVoiceOver[5])
            {
                //AudioManager.instance.PlayOneShot(FMODEvents.instance.tutorialVoiceOvers[8], player.transform.position);
                voiceOvers[5].stop(STOP_MODE.IMMEDIATE);
                voiceOvers[8].start();
                stepsVoiceOver[5] = true;
            }

            if (honer.GetComponent<Dirty>().dirtiness <= 0 && honer.GetComponent<Clean>().cleanness >= 100 &&
                knife.GetComponentInChildren<Dirty>().dirtiness <= 0 && knife.GetComponentInChildren<Clean>().cleanness >= 100)
            {
                instructionIndex++;
                //index++;
                //completeParticles[3].Play();
                steps[4] = false;
                steps[5] = true;
            }
        }
        if (steps[5])
        {
            //stepText.text = "Sanitize Tools";

            if (!stepsVoiceOver[6])
            {
                //AudioManager.instance.PlayOneShot(FMODEvents.instance.tutorialVoiceOvers[9], player.transform.position);
                voiceOvers[8].stop(STOP_MODE.IMMEDIATE);
                voiceOvers[9].start();
                stepsVoiceOver[6] = true;
            }

            if (honer.GetComponent<Dirty>().dirtiness <= 0 && honer.GetComponent<Clean>().cleanness <= 0 &&
                knife.GetComponentInChildren<Dirty>().dirtiness <= 0 && knife.GetComponentInChildren<Clean>().cleanness <= 0)
            {
                instructionIndex++;
                chapterIndex++;
                completeParticles[3].Play();
                steps[5] = false;
                steps[6] = true;
            }
        }
        if (steps[6])
        {
            //stepText.text = "Straighten Knife";

            if (!stepsVoiceOver[7])
            {
                //AudioManager.instance.PlayOneShot(FMODEvents.instance.tutorialVoiceOvers[11], player.transform.position);
                voiceOvers[9].stop(STOP_MODE.IMMEDIATE);
                voiceOvers[11].start();
                stepsVoiceOver[7] = true;
            }

            if (knife.GetComponent<KnifeStraighten>().strength >= 100.0f)
            {
                instructionIndex++;
                chapterIndex++;
                completeParticles[4].Play();
                steps[6] = false;
                steps[7] = true;
            }
        }
        if (steps[7])
        {
            //stepText.text = "Spawn and Cut Meat";

            if (!stepsVoiceOver[8])
            {
                //AudioManager.instance.PlayOneShot(FMODEvents.instance.tutorialVoiceOvers[13], player.transform.position);
                voiceOvers[11].stop(STOP_MODE.IMMEDIATE);
                voiceOvers[13].start();
                stepsVoiceOver[8] = true;
            }

            var cutMeat = FindObjectsOfType(typeof(CutMeat));
            if (cutMeat.Length > 0)
            {
                instructionIndex++;
                chapterIndex++;
                completeParticles[5].Play();
                steps[7] = false;
            }
        }
        if (chapterIndex == 4)
        {
            if (!stepsVoiceOver[9])
            {
                //AudioManager.instance.PlayOneShot(FMODEvents.instance.tutorialVoiceOvers[14], player.transform.position);
                voiceOvers[13].stop(STOP_MODE.IMMEDIATE);
                voiceOvers[14].start();
                stepsVoiceOver[9] = true;
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
