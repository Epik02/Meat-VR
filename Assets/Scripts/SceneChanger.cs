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

    private void OnCollisionEnter(Collision other)
    {

    }

    public void GameScene()
    {
        SceneManager.LoadScene("Game");
    }

    public void MenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LobbyScene()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void EndScene()
    {
        Debug.Log("Test");
        //SceneManager.LoadScene("SampleScene");
    }
}
