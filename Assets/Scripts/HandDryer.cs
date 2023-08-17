using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

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
            if (hands == null)
            {
                hands.Remove(item);
            }
        }
        if (hands.Count <= 0)
        {
            airParticle.Stop();
        }
        else
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

    private void UpdateSound()
    {
        PLAYBACK_STATE playbackState;
        airSound[0].getPlaybackState(out playbackState);

        if (hands.Count > 0)
        {
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                airSound[0].start();
                end = true;
            }
        }
        else
        {
            if (end)
            {
                airSound[0].stop(STOP_MODE.IMMEDIATE);
                airSound[1].start();
                end = false;
            }
        }
    }
}
