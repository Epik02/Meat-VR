using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class FileManager : MonoBehaviour
{
    public static FileManager instance;
    public List<int> highscores = new List<int>(6);
    public TMP_Text scoreText;

    private string filePath;

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
        filePath = Application.persistentDataPath + "/score.txt";                       // Retrieve the file path of the text file
        if (!File.Exists(filePath))                                                     // If file path does not exists...
        {
            File.CreateText(filePath).Dispose();                                        // Create the file path
            Write(filePath, true);                                                      // Write the startup scores (all 0's)
        }
        Debug.Log(filePath);
        Read(filePath);                                                                 // Read the scores in the text file

        if (scoreText != null)                                                          // If the leaderboard score text exists
        {
            Sort();                                                                     // Sort the scores from greatest to least
            string tempString = "";                                                     // Temporary string to hold all scores
            for (int i = 0; i < highscores.Capacity - 1; i++)
            {
                if (i < highscores.Capacity - 2)                                        // If the index is less then the second last value...
                {
                    tempString += (i + 1).ToString() + ") " + highscores[i] + "\n";     // Have the temporary string make a new line at the end of the format
                }
                else                                                                    // Otherwise...
                {
                    tempString += (i + 1).ToString() + ") " + highscores[i];            // Do not make a new line at the end
                }
            }
            scoreText.text = tempString;                                                // Make the leaderboard score text equal to the temporary string
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Updates the leaderboard
    public void UpdateScore(int score)
    {
        highscores.Add(score);                      // Adds new score
        Sort();                                     // Sorts from greatest to least
        highscores.RemoveAt(highscores.Count - 1);  // Removes the least score (so we only have a top 5)
    }

    // Sorts the leaderboard scores
    public void Sort()
    {
        highscores.Sort(SortByScore);
    }

    // Compares the scores to sort by
    private int SortByScore(int score1, int score2)
    {
        return score2.CompareTo(score1);
    }

    // Reads a file
    public void Read(string path)
    {
        StreamReader streamReader = new StreamReader(path);

        while (!streamReader.EndOfStream)                   // Reads line by line until there are no more lines
        {
            string line = streamReader.ReadLine();          // Takes a temporary line
            highscores.Add(int.Parse(line));                // Adds temporary line to scores by converting to int
        }

        streamReader.Close();                               // Always close the file when done
    }

    // Writes in a file
    public void Write(string path, bool newFile = false)
    {
        StreamWriter streamWriter = new StreamWriter(path);

        if (newFile)                                                // If a new file was just made...
        {
            for (int i = 0; i < highscores.Capacity - 1; i++)
            {
                streamWriter.WriteLine(0.ToString());               // Write 5 lines of 0's to start off the scores
            }
        }
        else                                                        // If it is not a new file...
        {
            foreach (var item in highscores)
            {
                streamWriter.WriteLine(item.ToString());            // Write the new scores in the file
            }
        }

        streamWriter.Close();                                       // Always close the file when done
    }
}
