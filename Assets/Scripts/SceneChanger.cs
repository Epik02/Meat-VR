using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void GameScene()
    {
        SceneManager.LoadScene("TestDemo");
    }

    public void MenuScene()
    {
        SceneManager.LoadScene("MenuDemo");
    }

    public void TutorialScene()
    {
        SceneManager.LoadScene("TutorialDemo");
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }
}
