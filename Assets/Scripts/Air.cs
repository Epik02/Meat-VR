using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Air : MonoBehaviour
{
    public float dryPerParticle = 0.5f;
    public ParticleSystem ps;

    private List<Wet> dryObjects;

    // Start is called before the first frame update
    void Start()
    {
        dryObjects = new List<Wet>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        Wet wet = other.GetComponent<Wet>();
        Wet wetChild = other.GetComponentInChildren<Wet>();

        if (other.CompareTag("Player"))
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
                    dryObjects.Add(wet);
                    ps.Play();
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
                    dryObjects.Add(wetChild);
                    ps.Play();
                }
            }
        }
    }
}
