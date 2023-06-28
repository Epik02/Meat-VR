using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.SceneManagement;

public class PhysicalButton : MonoBehaviour
{
    Vector3 localVelocity = new Vector3();
    Vector3 originalPos = new Vector3();
    public GameObject button;
    private float speed = 0;
    private float elapsedFrames;
    private bool runOnce = true;
    private Vector3 clickPos;
    bool intrigger = false;
    public Stopwatch timer = new Stopwatch();
    public string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        originalPos = button.transform.localPosition;
    }
    private void OnTriggerEnter(Collider other)
    {
        //when button hits trigger it lerps back to original position
        if (other.gameObject.name == "PhysicalButton")
        {
            speed = 1;
            intrigger = true;
            timer.Start();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (intrigger)
        {
            elapsedFrames += Time.deltaTime * speed;
            if (runOnce)
            {
                timer.Start();
                clickPos = button.transform.localPosition;
            }
            runOnce = false;
            UnityEngine.Debug.Log(clickPos);
            UnityEngine.Debug.Log(originalPos);
            button.transform.localPosition = Vector3.Lerp(clickPos, originalPos, elapsedFrames);
        }
        //resets timer when button returns to original position
        if (button.transform.localPosition.x >= originalPos.x)
        {
            UnityEngine.Debug.Log("hits it");
            speed = 0;
            intrigger = false;
            runOnce = true;
            elapsedFrames = 0;
            button.transform.localPosition = originalPos + new Vector3(0.01f, 0, 0);
        }

        //switches scenes
        if (timer.Elapsed.TotalSeconds >= 2)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
