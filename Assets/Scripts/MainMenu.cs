using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    private bool loadLobby = false;
    private bool exitGame = false;
    public string scene;
    // Start is called before the first frame update
    void Start()
    {
        loadLobby = false;
        exitGame = false;
    }

    public void enableStart()
    {
        loadLobby = true;
    }

    public void enableExit()
    {
        exitGame = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (loadLobby)
        {
            SceneManager.LoadScene(scene);
        }

        if (exitGame)
        {
            Application.Quit();
        }
    }
}
