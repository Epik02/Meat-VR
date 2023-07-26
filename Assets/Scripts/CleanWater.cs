using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CleanWater : MonoBehaviour
{
    public ParticleSystem finishedParticle;
    public float cleanPerParticle = 0.1f;

    private List<Dirty> dirtyObjects;
    private List<Clean> cleanObjects;
    private List<Wet> wetObjects;

    // Start is called before the first frame update
    void Start()
    {
        dirtyObjects = new List<Dirty>();
        cleanObjects = new List<Clean>();
        wetObjects = new List<Wet>();
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

        Wet wet = other.GetComponent<Wet>();
        Wet wetChild = other.GetComponentInChildren<Wet>();

        //if (dirty)
        //{
        //    if (!dirtyObjects.Contains(dirty))
        //    {
        //        dirtyObjects.Add(dirty);
        //    }
        //    if (dirty.dirtiness > 0.0f)
        //    {
        //        dirtyObjects.Remove(dirty);
        //        warningPanel.SetActive(false);
        //        dirty.dirtiness -= cleanPerParticle;
        //    }
        //    if (!dirtyObjects.Contains(dirty) && dirty.dirtiness <= 0.0f)
        //    {
        //        dirtyObjects.Add(dirty);
        //        finishedParticle.Play();
        //    }
        //}

        //if (dirtyChild)
        //{
        //    if (!dirtyObjects.Contains(dirtyChild))
        //    {
        //        dirtyObjects.Add(dirtyChild);
        //    }
        //    if (dirtyChild.dirtiness > 0.0f)
        //    {
        //        dirtyObjects.Remove(dirtyChild);
        //        warningPanel.SetActive(false);
        //        dirtyChild.dirtiness -= cleanPerParticle;
        //    }
        //    if (!dirtyObjects.Contains(dirtyChild) && dirtyChild.dirtiness <= 0.0f)
        //    {
        //        dirtyObjects.Add(dirtyChild);
        //        finishedParticle.Play();
        //    }
        //}

        if (clean && wet)
        {
            if (!wetObjects.Contains(wet))
            {
                wetObjects.Add(wet);
            }
            if (clean.cleanness > 0.0f && dirty.dirtiness <= 0.0f)
            {
                wetObjects.Remove(wet);
                clean.cleanness -= cleanPerParticle;
            }
            if (wet.wetness < 100.0f && dirty.dirtiness <= 0.0f)
            {
                wetObjects.Remove(wet);
                wet.wetness += cleanPerParticle;
            }
            if (!wetObjects.Contains(wet) && clean.cleanness <= 0.0f && wet.wetness >= 100.0f)
            {
                wetObjects.Add(wet);
                finishedParticle.Play();
            }
        }

        if (cleanChild && wetChild)
        {
            if (!wetObjects.Contains(wetChild))
            {
                wetObjects.Add(wetChild);
            }
            if (cleanChild.cleanness > 0.0f && dirtyChild.dirtiness <= 0.0f)
            {
                wetObjects.Remove(wetChild);
                cleanChild.cleanness -= cleanPerParticle;
            }
            if (wetChild.wetness < 100.0f && dirtyChild.dirtiness <= 0.0f)
            {
                wetObjects.Remove(wetChild);
                wetChild.wetness += cleanPerParticle;
            }
            if (!wetObjects.Contains(wetChild) && cleanChild.cleanness <= 0.0f && wetChild.wetness >= 100.0f)
            {
                wetObjects.Add(wetChild);
                finishedParticle.Play();
            }
        }

        if (clean && wet == null)
        {
            if (!cleanObjects.Contains(clean))
            {
                cleanObjects.Add(clean);
            }
            if (clean.cleanness > 0.0f)
            {
                cleanObjects.Remove(clean);
                clean.cleanness -= cleanPerParticle;
            }
            if (!cleanObjects.Contains(clean) && clean.cleanness <= 0.0f)
            {
                cleanObjects.Add(clean);
                finishedParticle.Play();
            }
        }

        if (cleanChild && wetChild == null)
        {
            if (!cleanObjects.Contains(cleanChild))
            {
                cleanObjects.Add(cleanChild);
            }
            if (cleanChild.cleanness > 0.0f)
            {
                cleanObjects.Remove(cleanChild);
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
