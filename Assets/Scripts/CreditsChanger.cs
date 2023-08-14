using Autohand.Demo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.XR.OpenXR.Features.Interactions.HTCViveControllerProfile;

public class CreditsChanger : MonoBehaviour
{
    public GameObject menuPage;
    public GameObject credits;

    private bool creditsOn = false;

    // Start is called before the first frame update
    void Start()
    {
        menuPage.SetActive(!creditsOn);
        credits.SetActive(creditsOn);
    }

    public void Credits()
    {
        creditsOn = !creditsOn;
        menuPage.SetActive(!creditsOn);
        credits.SetActive(creditsOn);
    }
}
