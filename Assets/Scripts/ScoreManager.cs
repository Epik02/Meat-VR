using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    
    private int totalScore;

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

    // Gets the total score
    public int GetScore()
    {
        return totalScore;
    }

    // Adds to the total score
    public void AddScore(int score)
    {
        totalScore += score;
    }

    // Removes from the total score
    public void RemoveScore(int score) 
    {
        totalScore -= score;
    }
}
