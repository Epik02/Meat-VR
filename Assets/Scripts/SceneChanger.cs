using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void GameScene()
    {
        FMOD.ChannelGroup mcg;
        FMODUnity.RuntimeManager.CoreSystem.getMasterChannelGroup(out mcg);
        mcg.stop();
        SceneManager.LoadScene("TestDemo");
    }

    public void MenuScene()
    {
        FMOD.ChannelGroup mcg;
        FMODUnity.RuntimeManager.CoreSystem.getMasterChannelGroup(out mcg);
        mcg.stop();
        SceneManager.LoadScene("MenuDemo");
    }

    public void TutorialScene()
    {
        FMOD.ChannelGroup mcg;
        FMODUnity.RuntimeManager.CoreSystem.getMasterChannelGroup(out mcg);
        mcg.stop();
        SceneManager.LoadScene("TutorialDemo");
    }

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
