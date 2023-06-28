using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    //public enum SceneToChangeTo
    //{
    //    Game,
    //    Menu,
    //    Lobby,
    //    End
    //};

    //public SceneToChangeTo sceneToChangeTo;

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
        //switch (sceneToChangeTo)
        //{
        //    case SceneToChangeTo.Game:
        //        GameScene();
        //        break;
        //    case SceneToChangeTo.Menu:
        //        MenuScene();
        //        break;
        //    case SceneToChangeTo.Lobby:
        //        LobbyScene(); 
        //        break;
        //    case SceneToChangeTo.End:
        //        EndScene();
        //        break;
        //    default:
        //        break;
        //}
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
        //SceneManager.LoadScene("SampleScene");
    }
}
