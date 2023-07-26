using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Soap : MonoBehaviour
{
    public float soapPerParticle;
    public ParticleSystem ps;

    private List<Clean> soapObjects;

    // Start is called before the first frame update
    void Start()
    {
        soapObjects = new List<Clean>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnParticleCollision(GameObject other)
    {
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
                ps.Play();
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
                ps.Play();
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
