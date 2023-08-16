using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WetSoap : MonoBehaviour
{
    public float dryPerParticle = 0.5f;
    public List<GameObject> finishedParticles;

    private List<Wet> dryObjects;
    private ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        var follows = FindObjectsOfType(typeof(ParticleFollow));
        foreach (var item in follows)
        {
            finishedParticles.Add(item.GameObject());
        }
        ps = GetComponent<ParticleSystem>();
        dryObjects = new List<Wet>();
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

        Wet wet = other.GetComponent<Wet>();
        Wet wetChild = other.GetComponentInChildren<Wet>();

        if (other.CompareTag("Item"))
        {
            if (wet)
            {
                if (!dryObjects.Contains(wet))
                {
                    dryObjects.Add(wet);
                }
                if (wet.wetness > 0.0f)
                {
                    dryObjects.Remove(wet);
                    wet.wetness -= dryPerParticle;
                }
                if (!dryObjects.Contains(wet) && wet.wetness >= 100.0f)
                {
                    if (ScoreManager.instance)
                    {
                        ScoreManager.instance.AddScore(5);
                    }
                    dryObjects.Add(wet);
                    p.Play();
                }
            }

            if (wetChild)
            {
                if (!dryObjects.Contains(wetChild))
                {
                    dryObjects.Add(wetChild);
                }
                if (wetChild.wetness > 0.0f)
                {
                    dryObjects.Remove(wetChild);
                    wetChild.wetness -= dryPerParticle;
                }
                if (!dryObjects.Contains(wetChild) && wetChild.wetness <= 0.0f)
                {
                    if (ScoreManager.instance)
                    {
                        ScoreManager.instance.AddScore(5);
                    }
                    dryObjects.Add(wetChild);
                    p.Play();
                }
            }
        }
    }
}
