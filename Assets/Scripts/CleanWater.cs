using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CleanWater : MonoBehaviour
{
    public GameObject warningPanel;
    public ParticleSystem finishedParticle;
    public float cleanPerParticle = 0.1f;

    private List<Dirty> cleanObjects;

    // Start is called before the first frame update
    void Start()
    {
        cleanObjects = new List<Dirty>();
        finishedParticle.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        Dirty dirty = other.GetComponent<Dirty>();
        Dirty dirtyChild = other.GetComponentInChildren<Dirty>();

        if (dirty)
        {
            if (!cleanObjects.Contains(dirty))
            {
                cleanObjects.Add(dirty);
            }
            if (dirty.dirtiness > 0.0f)
            {
                cleanObjects.Remove(dirty);
                warningPanel.SetActive(false);
                dirty.dirtiness -= cleanPerParticle;
            }
            if (!cleanObjects.Contains(dirty) && dirty.dirtiness <= 0.0f)
            {
                cleanObjects.Add(dirty);
                finishedParticle.Play();
            }
        }

        if (dirtyChild)
        {
            if (!cleanObjects.Contains(dirtyChild))
            {
                cleanObjects.Add(dirtyChild);
            }
            if (dirtyChild.dirtiness > 0.0f)
            {
                cleanObjects.Remove(dirtyChild);
                warningPanel.SetActive(false);
                dirtyChild.dirtiness -= cleanPerParticle;
            }
            if (!cleanObjects.Contains(dirtyChild) && dirtyChild.dirtiness <= 0.0f)
            {
                cleanObjects.Add(dirtyChild);
                finishedParticle.Play();
            }
        }
    }
}
