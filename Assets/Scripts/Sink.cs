using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sink : MonoBehaviour
{
    public ParticleSystem waterParticle;

    private List<GameObject> hands = new List<GameObject>();
    private EventInstance sinkSound;

    // Start is called before the first frame update
    void Start()
    {
        waterParticle.Stop();
        sinkSound = AudioManager.instance.CreateInstance(FMODEvents.instance.sink);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSound();
    }

    private void OnTriggerEnter(Collider other)
    {
        hands.Add(other.gameObject);
        waterParticle.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        hands.Remove(other.gameObject);
        if (hands.Count <= 0)
        {
            waterParticle.Stop();
        }
    }

    private void UpdateSound()
    {
        if (hands.Count > 0)
        {
            PLAYBACK_STATE playbackState;
            sinkSound.getPlaybackState(out playbackState);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                sinkSound.start();
            }
        }
        else
        {
            sinkSound.stop(STOP_MODE.ALLOWFADEOUT);
        }
    }
}
