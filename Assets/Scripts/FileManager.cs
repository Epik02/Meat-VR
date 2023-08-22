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
        filePath = Application.persistentDataPath + "/score.txt";
        if (!File.Exists(filePath))
        {
            File.CreateText(filePath).Dispose();
            Write(filePath, true);
        }
        Debug.Log(filePath);
        Read(filePath);

        if (scoreText != null)
        {
            Sort();
            string tempString = "";
            for (int i = 0; i < highscores.Capacity - 1; i++)
            {
                if (i < highscores.Capacity - 2)
                {
                    tempString += (i + 1).ToString() + ") " + highscores[i] + "\n";
                }
                else
                {
                    tempString += (i + 1).ToString() + ") " + highscores[i];
                }
            }
            scoreText.text = tempString;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateScore(int score)
    {
        highscores.Add(score);
        Sort();
        highscores.RemoveAt(highscores.Count - 1);
    }

    public void Sort()
    {
        highscores.Sort(SortByScore);
    }

    private int SortByScore(int score1, int score2)
    {
        return score2.CompareTo(score1);
    }

    public void Read(string path)
    {
        StreamReader streamReader = new StreamReader(path);

        while (!streamReader.EndOfStream)
        {
            string line = streamReader.ReadLine();
            highscores.Add(int.Parse(line));
        }

        streamReader.Close();
    }

    public void Write(string path, bool newFile = false)
    {
        StreamWriter streamWriter = new StreamWriter(path);

        if (newFile)
        {
            for (int i = 0; i < highscores.Capacity - 1; i++)
            {
                streamWriter.WriteLine(0.ToString());
            }
        }
        else
        {
            foreach (var item in highscores)
            {
                streamWriter.WriteLine(item.ToString());
            }
        }

        streamWriter.Close();
    }
}
