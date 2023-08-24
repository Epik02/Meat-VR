using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class SoapDispenser : MonoBehaviour
{
    public ParticleSystem soapParticle;

    private List<GameObject> hands = new List<GameObject>();
    private EventInstance soapSound;

    // Start is called before the first frame update
    void Start()
    {
        soapParticle.Stop();
        soapSound = AudioManager.instance.CreateInstance(FMODEvents.instance.soapDispenser);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSound();
    }

    private void OnTriggerEnter(Collider other)
    {
        // When objects are under the sensor it will play the soap particle
        hands.Add(other.gameObject);
        soapParticle.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        // When there are no objects under the sensor it will stop the soap particle
        hands.Remove(other.gameObject);
        if (hands.Count <= 0)
        {
            soapParticle.Stop();
        }
    }

    // Updates the sound of the soap dispenser when there are objects under the sensor or not
    private void UpdateSound()
    {
        if (hands.Count > 0)
        {
            PLAYBACK_STATE playbackState;
            soapSound.getPlaybackState(out playbackState);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                soapSound.start();
            }
        }
        else
        {
            soapSound.stop(STOP_MODE.ALLOWFADEOUT);
        }
    }
}
