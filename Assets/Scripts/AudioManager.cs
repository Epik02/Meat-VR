using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;       // Create instance

    void Awake()
    {
        if (instance == null)                  // If instance does not exist then this will be the instnace
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Plays a sound once with FMOD Studio
    public void PlayOneShot(EventReference sound, Vector3 worldPosition)
    {
        RuntimeManager.PlayOneShot(sound, worldPosition);
    }

    // Creates a sound instance with FMOD studio
    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance ei = RuntimeManager.CreateInstance(eventReference);
        return ei;
    }
}
