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
        // When objects are under the sensor it will play the water particle
        hands.Add(other.gameObject);
        waterParticle.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        // When there are no objects under the sensor it will stop the water particle
        hands.Remove(other.gameObject);
        if (hands.Count <= 0)
        {
            waterParticle.Stop();
        }
    }

    // Updates the sound of the sink when there are objects under the sensor or not
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
            sinkSound.stop(STOP_MODE.IMMEDIATE);
        }
    }
}
