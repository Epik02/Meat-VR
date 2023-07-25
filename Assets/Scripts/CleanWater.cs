using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CleanWater : MonoBehaviour
{
    public GameObject warningPanel;
    public ParticleSystem finishedParticle;
    public float cleanPerParticle = 0.1f;

    private List<Dirty> dirtyObjects;
    private List<Clean> cleanObjects;

    // Start is called before the first frame update
    void Start()
    {
        dirtyObjects = new List<Dirty>();
        cleanObjects = new List<Clean>();
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

        Clean clean = other.GetComponent<Clean>();
        Clean cleanChild = other.GetComponentInChildren<Clean>();

        if (dirty)
        {
            if (!dirtyObjects.Contains(dirty))
            {
                dirtyObjects.Add(dirty);
            }
            if (dirty.dirtiness > 0.0f)
            {
                dirtyObjects.Remove(dirty);
                warningPanel.SetActive(false);
                dirty.dirtiness -= cleanPerParticle;
            }
            if (!dirtyObjects.Contains(dirty) && dirty.dirtiness <= 0.0f)
            {
                dirtyObjects.Add(dirty);
                finishedParticle.Play();
            }
        }

        if (dirtyChild)
        {
            if (!dirtyObjects.Contains(dirtyChild))
            {
                dirtyObjects.Add(dirtyChild);
            }
            if (dirtyChild.dirtiness > 0.0f)
            {
                dirtyObjects.Remove(dirtyChild);
                warningPanel.SetActive(false);
                dirtyChild.dirtiness -= cleanPerParticle;
            }
            if (!dirtyObjects.Contains(dirtyChild) && dirtyChild.dirtiness <= 0.0f)
            {
                dirtyObjects.Add(dirtyChild);
                finishedParticle.Play();
            }
        }

        if (clean)
        {
            if (!cleanObjects.Contains(clean))
            {
                cleanObjects.Add(clean);
            }
            if (clean.cleanness > 0.0f)
            {
                cleanObjects.Remove(clean);
                warningPanel.SetActive(false);
                clean.cleanness -= cleanPerParticle;
            }
            if (!cleanObjects.Contains(clean) && clean.cleanness <= 0.0f)
            {
                cleanObjects.Add(clean);
                finishedParticle.Play();
            }
        }

        if (cleanChild)
        {
            if (!cleanObjects.Contains(cleanChild))
            {
                cleanObjects.Add(cleanChild);
            }
            if (cleanChild.cleanness > 0.0f)
            {
                cleanObjects.Remove(cleanChild);
                warningPanel.SetActive(false);
                cleanChild.cleanness -= cleanPerParticle;
            }
            if (!cleanObjects.Contains(cleanChild) && cleanChild.cleanness <= 0.0f)
            {
                cleanObjects.Add(cleanChild);
                finishedParticle.Play();
            }
        }
    }
}
