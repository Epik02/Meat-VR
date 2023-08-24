using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandDryer : MonoBehaviour
{
    public ParticleSystem airParticle;

    private List<GameObject> hands = new List<GameObject>();
    private List<EventInstance> airSound = new List<EventInstance>();
    private bool end = false;

    // Start is called before the first frame update
    void Start()
    {
        airParticle.Stop();
        for (int i = 0; i < FMODEvents.instance.airDryer.Count; i++)
        {
            airSound.Add(AudioManager.instance.CreateInstance(FMODEvents.instance.airDryer[i]));
        }
        float temp;
        airSound[1].getVolume(out temp);
        airSound[1].setVolume(temp * 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSound();
        foreach (var item in hands)
        {
            if (hands == null)      // Check if the hands are still in the sensor otherwise remove it
            {
                hands.Remove(item);
            }
        }
        if (hands.Count <= 0)
        {
            airParticle.Stop();
        }
        else                        // Play particles when hands are in the sensor
        {
            airParticle.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hands.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hands.Remove(other.gameObject);
        }
    }

    // Update the sound from part 1 to part 2
    private void UpdateSound()
    {
        PLAYBACK_STATE playbackState;
        airSound[0].getPlaybackState(out playbackState);

        if (hands.Count > 0)                                    // If there are hands on the hand dryer...
        {
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))   // If sound is not playing
            {
                airSound[0].start();                            // Then play air sound
                end = true;                                     // End part is true
            }
        }
        else                                                    // If there are no hands on the hand dryer...
        {
            if (end)                                            // If end part is true
            {
                airSound[0].stop(STOP_MODE.IMMEDIATE);          // Stop the first sound part
                airSound[1].start();                            // Start the second sound part
                end = false;                                    // End part is false
            }
        }
    }
}
