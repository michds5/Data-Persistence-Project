using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NameScoreManager : MonoBehaviour
{
    public static NameScoreManager instance;
    public List<Top10ScoreData> topScores = new List<Top10ScoreData>();

    public string playerName;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        LoadTopScores();
    }

    // Checks if a score should be added. If so, it is added to the
    // appropriate spot in the topScores list (descending order).
    int maxNumTopScores = 10;
    public void AddScore(string name, int score)
    { 
        // If topScores list is at its max amount of scores and score is
        // greater than the bottom score, then remove the bottom score.
        if (topScores.Count == maxNumTopScores)
        {
            if (score > topScores[topScores.Count - 1].top10Score)
            {
                topScores.RemoveAt(topScores.Count - 1);
            }
            else
            {
                return;
            }
        }

        // Add new score to the appropriate location in the topScores list
        // (above the scores that are lesser, and below the scores that are
        // greater or equal)
        if (topScores.Count > 0)
        {
            for (int i = 0; i < topScores.Count; i++)
            {
                if (score > topScores[i].top10Score)
                {
                    topScores.Insert(i, new Top10ScoreData() { top10ScoreName = name, top10Score = score });
                    return;
                }
            }
        }
        // Add score when topScores is empty
        topScores.Add(new Top10ScoreData() { top10ScoreName = name, top10Score = score });

    }

    [System.Serializable]
    class SaveData
    {
        public List<Top10ScoreData> topScores;
    }

    // Save best scorer and their score for future sessions
    public void SaveTopScores()
    {
        SaveData data = new SaveData();
        data.topScores = topScores;
        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    // Load best scorer and their score from previous save
    public void LoadTopScores()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            topScores = data.topScores;
        }
    }
}

