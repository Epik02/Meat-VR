using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameScene()
    {
        SceneManager.LoadScene("TestDemo");
    }

    public void MenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LobbyScene()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }

    public void EndScene()
    {
        //SceneManager.LoadScene("SampleScene");
    }
}
