using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsChanger : MonoBehaviour
{
    public GameObject menuPage;
    public GameObject credits;

    private bool creditsOn = false;

    // Start is called before the first frame update
    void Start()
    {
        menuPage.SetActive(!creditsOn);     // Set menu page to opposite of creditsOn value
        credits.SetActive(creditsOn);       // Set credits page to creditsOn value
    }

    // Switches boolean value when button is pressed
    public void Credits()
    {
        creditsOn = !creditsOn;             // Switch creditsOn value to opposite of original value
        menuPage.SetActive(!creditsOn);
        credits.SetActive(creditsOn);
    }
}
