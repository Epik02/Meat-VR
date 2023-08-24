using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Change to game scene
    public void GameScene()
    {
        // Make sure all sound has stopped when switching scenes
        FMOD.ChannelGroup mcg;
        FMODUnity.RuntimeManager.CoreSystem.getMasterChannelGroup(out mcg);
        mcg.stop(); 
        SceneManager.LoadScene("GameDemo");
    }

    // Change to menu scene
    public void MenuScene()
    {
        FMOD.ChannelGroup mcg;
        FMODUnity.RuntimeManager.CoreSystem.getMasterChannelGroup(out mcg);
        mcg.stop();
        SceneManager.LoadScene("MenuDemo");
    }

    // Change to tutorial scene
    public void TutorialScene()
    {
        FMOD.ChannelGroup mcg;
        FMODUnity.RuntimeManager.CoreSystem.getMasterChannelGroup(out mcg);
        mcg.stop();
        SceneManager.LoadScene("TutorialDemo");
    }

    // Exit game (either from editor or main application)
    public void ExitGame()
    {
        FMOD.ChannelGroup mcg;
        FMODUnity.RuntimeManager.CoreSystem.getMasterChannelGroup(out mcg);
        mcg.stop();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }
}
