using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WarningCollision : MonoBehaviour
{
    public GameObject warningPanel;
    public TMP_Text warningText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        Dirty dirty = GetComponent<Dirty>();
        Dirty dirtyChild = GetComponentInChildren<Dirty>();

        Clean clean = GetComponent<Clean>();
        Clean cleanChild = GetComponentInChildren<Clean>();

        Wet wet = GetComponent<Wet>();
        Wet wetChild = GetComponentInChildren<Wet>();

        if (dirty)
        {
            if (dirty.dirtiness > 0.0f)
            {
                if (other.gameObject.tag == "Item" && gameObject.tag == "Player")
                {
                    warningPanel.SetActive(true);
                    warningText.text = "Warning: Must have clean hands before using equipment";
                }
                if (other.gameObject.tag == "Item" && gameObject.tag == "Item")
                {
                    warningPanel.SetActive(true);
                    warningText.text = "Warning: Must have clean equipment before using them";
                }
                if (other.gameObject.tag == "cuttable" && gameObject.tag == "Item")
                {
                    warningPanel.SetActive(true);
                    warningText.text = "Warning: Must have clean equipment before using them";
                }
            }
        }
        if (dirtyChild)
        {
            if (dirtyChild.dirtiness > 0.0f)
            {
                if (other.gameObject.tag == "Item" && gameObject.tag == "Player")
                {
                    warningPanel.SetActive(true);
                    warningText.text = "Warning: Must have clean hands before using equipment";
                }
                if (other.gameObject.tag == "Item" && gameObject.tag == "Item")
                {
                    warningPanel.SetActive(true);
                    warningText.text = "Warning: Must have clean equipment before using them";
                }
                if (other.gameObject.tag == "cuttable" && gameObject.tag == "Item")
                {
                    warningPanel.SetActive(true);
                    warningText.text = "Warning: Must have clean equipment before using them";
                }
            }
        }

        if (clean)
        {
            if (clean.cleanness > 0.0f)
            {
                if (other.gameObject.tag == "Item" && gameObject.tag == "Player")
                {
                    warningPanel.SetActive(true);
                    warningText.text = "Warning: Must have clean hands before using equipment";
                }
                if (other.gameObject.tag == "Item" && gameObject.tag == "Item")
                {
                    warningPanel.SetActive(true);
                    warningText.text = "Warning: Must have clean equipment before using them";
                }
                if (other.gameObject.tag == "cuttable" && gameObject.tag == "Item")
                {
                    warningPanel.SetActive(true);
                    warningText.text = "Warning: Must have clean equipment before using them";
                }
            }
        }
        if (cleanChild)
        {
            if (cleanChild.cleanness > 0.0f)
            {
                if (other.gameObject.tag == "Item" && gameObject.tag == "Player")
                {
                    warningPanel.SetActive(true);
                    warningText.text = "Warning: Must have clean hands before using equipment";
                }
                if (other.gameObject.tag == "Item" && gameObject.tag == "Item")
                {
                    warningPanel.SetActive(true);
                    warningText.text = "Warning: Must have clean equipment before using them";
                }
                if (other.gameObject.tag == "cuttable" && gameObject.tag == "Item")
                {
                    warningPanel.SetActive(true);
                    warningText.text = "Warning: Must have clean equipment before using them";
                }
            }
        }

        if (wet)
        {
            if (wet.wetness > 0.0f)
            {
                if (other.gameObject.tag == "Item" && gameObject.tag == "Player")
                {
                    warningPanel.SetActive(true);
                    warningText.text = "Warning: Must have clean hands before using equipment";
                }
                if (other.gameObject.tag == "Item" && gameObject.tag == "Item")
                {
                    warningPanel.SetActive(true);
                    warningText.text = "Warning: Must have clean equipment before using them";
                }
                if (other.gameObject.tag == "cuttable" && gameObject.tag == "Item")
                {
                    warningPanel.SetActive(true);
                    warningText.text = "Warning: Must have clean equipment before using them";
                }
            }
        }
        if (wetChild)
        {
            if (wetChild.wetness > 0.0f)
            {
                if (other.gameObject.tag == "Item" && gameObject.tag == "Player")
                {
                    warningPanel.SetActive(true);
                    warningText.text = "Warning: Must have clean hands before using equipment";
                }
                if (other.gameObject.tag == "Item" && gameObject.tag == "Item")
                {
                    warningPanel.SetActive(true);
                    warningText.text = "Warning: Must have clean equipment before using them";
                }
                if (other.gameObject.tag == "cuttable" && gameObject.tag == "Item")
                {
                    warningPanel.SetActive(true);
                    warningText.text = "Warning: Must have clean equipment before using them";
                }
            }
        }
    }
}
