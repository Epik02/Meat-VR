using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class WarningPanel : MonoBehaviour
{
    private bool startTimer = false;
    private float timer = 0.0f;
    private float maxTime = 5.0f;

    private void OnEnable()
    {
        startTimer = true;
    }

    private void OnDisable()
    {
        startTimer = false;
        timer = 0.0f;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (startTimer)
        {
            timer += Time.deltaTime;
            if (timer >= maxTime)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
