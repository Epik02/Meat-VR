using Autohand;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialSystem : MonoBehaviour
{
    public List<ParticleSystem> completeParticles;
    public Image[] checkmarks;
    public TMP_Text stepText;
    public GameObject[] tools;
    public GameObject gloves;
    public GameObject player;

    private bool[] steps = new bool[6] { false, false, false, false, false, false };
    private GameObject honer;
    private GameObject knife;
    private int index;
    private bool isGrabbed = false;
    private Vector3 playerOriginalPosition;
    private Quaternion playerOriginalRotation;

    // Start is called before the first frame update
    void Start()
    {
        index = -1;
        steps[0] = true;
        honer = tools[0];
        knife = tools[1];
        playerOriginalPosition = player.transform.position;
        playerOriginalRotation = player.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        CheckmarkDisplay();

        if (steps[0])
        {
            stepText.text = "Move and Rotate";

            float distance = Vector3.Distance(player.transform.position, playerOriginalPosition);
            if (distance > 1.0f && player.transform.rotation != playerOriginalRotation)
            {
                index++;
                completeParticles[0].Play();
                steps[0] = false;
                steps[1] = true;
            }
        }
        if (steps[1])
        {
            stepText.text = "Pick Up Tools";

            if (isGrabbed)
            {
                index++;
                completeParticles[1].Play();
                steps[1] = false;
                steps[2] = true;
            }
        }
        if (steps[2])
        {
            stepText.text = "Wash Hands and Wear Gloves";

            if (gloves == null)
            {
                index++;
                completeParticles[2].Play();
                steps[2] = false;
                steps[3] = true;
            }
        }
        if (steps[3])
        {
            stepText.text = "Sanitize Tools";

            if (honer.GetComponent<Dirty>().dirtiness <= 0 && honer.GetComponent<Clean>().cleanness <= 0 &&
                knife.GetComponentInChildren<Dirty>().dirtiness <= 0 && knife.GetComponentInChildren<Clean>().cleanness <= 0)
            {
                index++;
                completeParticles[3].Play();
                steps[3] = false;
                steps[4] = true;
            }
        }
        if (steps[4])
        {
            stepText.text = "Straighten Knife";

            if (knife.GetComponent<KnifeStraighten>().strength >= 100.0f)
            {
                index++;
                completeParticles[4].Play();
                steps[4] = false;
                steps[5] = true;
            }
        }
        if (steps[5])
        {
            stepText.text = "Spawn and Cut Meat";

            var cutMeat = FindObjectsOfType(typeof(CutMeat));
            if (cutMeat.Length > 0)
            {
                index++;
                completeParticles[5].Play();
                steps[5] = false;
            }
        }
        if (index == 5)
        {
            stepText.text = "Tutorial Over";
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
