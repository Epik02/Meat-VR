using Autohand;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CleanSoap : MonoBehaviour
{
    public float soapPerParticle;
    public List<GameObject> finishedParticles;

    private ParticleSystem ps;
    private List<Clean> soapObjects;

    // Start is called before the first frame update
    void Start()
    {
        var follows = FindObjectsOfType(typeof(ParticleFollow));
        foreach (var item in follows)
        {
            finishedParticles.Add(item.GameObject());
        }
        ps = GetComponent<ParticleSystem>();
        soapObjects = new List<Clean>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ps != null)
        {
            if (ps.isStopped)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        ParticleSystem p = new ParticleSystem();

        foreach (var item in finishedParticles)
        {
            if (item.GetComponent<ParticleFollow>().target != null)
            {
                if (item.GetComponent<ParticleFollow>().target.gameObject == other)
                {
                    p = item.GetComponent<ParticleSystem>();
                }
            }
        }

        Clean clean = other.GetComponent<Clean>();
        Clean cleanChild = other.GetComponentInChildren<Clean>();

        Dirty dirty = other.GetComponent<Dirty>();
        Dirty dirtyChild = other.GetComponentInChildren<Dirty>();

        if (clean)
        {
            if (!soapObjects.Contains(clean))
            {
                soapObjects.Add(clean);
            }
            if (clean.cleanness < 100.0f)
            {
                soapObjects.Remove(clean);
                clean.cleanness += soapPerParticle;
            }
            if (dirty.dirtiness > 0.0f)
            {
                soapObjects.Remove(clean);
                dirty.dirtiness -= soapPerParticle;
            }
            if (!soapObjects.Contains(clean) && clean.cleanness >= 100.0f && dirty.dirtiness <= 0.0f)
            {
                soapObjects.Add(clean);
                p.Play();
            }
        }

        if (cleanChild)
        {
            if (!soapObjects.Contains(cleanChild))
            {
                soapObjects.Add(clean);
            }
            if (cleanChild.cleanness < 100.0f)
            {
                soapObjects.Remove(clean);
                cleanChild.cleanness += soapPerParticle;
            }
            if (dirtyChild.dirtiness > 0.0f)
            {
                soapObjects.Remove(cleanChild);
                dirtyChild.dirtiness -= soapPerParticle;
            }
            if (!soapObjects.Contains(cleanChild) && cleanChild.cleanness >= 100.0f && dirtyChild.dirtiness <= 0.0f)
            {
                soapObjects.Add(cleanChild);
                p.Play();
            }
        }

        //if (clean)
        //{
        //    if (clean.cleanness < 100.0f)
        //    {
        //        clean.cleanness += soapPerParticle;
        //    }
        //    if (dirty.dirtiness > 0.0f)
        //    {
        //        dirty.dirtiness -= soapPerParticle;
        //    }
        //}

        //if (cleanChild)
        //{
        //    if (cleanChild.cleanness < 100.0f)
        //    {
        //        cleanChild.cleanness += soapPerParticle;
        //    }
        //    if (dirtyChild.dirtiness > 0.0f)
        //    {
        //        dirtyChild.dirtiness -= soapPerParticle;
        //    }
        //}
    }
}