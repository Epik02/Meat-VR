using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public enum SceneToChangeTo
    {
        Game,
        Menu,
        End
    };

    public SceneToChangeTo sceneToChangeTo;

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
        switch (sceneToChangeTo)
        {
            case SceneToChangeTo.Game:
                GameScene();
                break;
            case SceneToChangeTo.Menu:
                MenuScene();
                break;
            case SceneToChangeTo.End:
                EndScene();
                break;
            default:
                break;
        }
    }

    public void GameScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void MenuScene()
    {
        //SceneManager.LoadScene("SampleScene");
    }

    public void EndScene()
    {
        //SceneManager.LoadScene("SampleScene");
    }
}
