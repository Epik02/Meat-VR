using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandDryer : MonoBehaviour
{
    public ParticleSystem airParticle;

    private List<GameObject> hands = new List<GameObject>();
    private EventInstance airSound;

    // Start is called before the first frame update
    void Start()
    {
        airParticle.Stop();
        airSound = AudioManager.instance.CreateInstance(FMODEvents.instance.airDryer);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSound();
    }

    private void OnTriggerEnter(Collider other)
    {
        hands.Add(other.gameObject);
        airParticle.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        hands.Remove(other.gameObject);
        if (hands.Count <= 0)
        {
            airParticle.Stop();
        }
    }

    private void UpdateSound()
    {
        if (hands.Count > 0)
        {
            PLAYBACK_STATE playbackState;
            airSound.getPlaybackState(out playbackState);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                airSound.start();
            }
        }
        else
        {
            airSound.stop(STOP_MODE.ALLOWFADEOUT);
        }
    }
}
