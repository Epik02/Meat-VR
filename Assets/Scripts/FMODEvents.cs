using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{
    [SerializeField] public EventReference soapDispenser;
    [SerializeField] public EventReference sink;
    [SerializeField] public EventReference airDryer;
    [SerializeField] public EventReference honing;
    [SerializeField] public EventReference sprayBottle;
    [SerializeField] public List<EventReference> tutorialVoiceOvers;
    [SerializeField] public List<EventReference> gameVoiceOvers;

    public static FMODEvents instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
