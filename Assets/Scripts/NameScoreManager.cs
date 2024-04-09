using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NameScoreManager : MonoBehaviour
{
    public static NameScoreManager instance;
    public List<BestScoreData> bestScores = new List<BestScoreData>();

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
        LoadBestScores();
    }

    public void AddScore(string name, int score)
    {
        
        if (bestScores.Count >= 10)
        {
            if (score > bestScores[bestScores.Count - 1].bestScore)
            {
                bestScores.RemoveAt(bestScores.Count - 1);
            }
            else
            {
                return;
            }
        }
        if (bestScores.Count > 0)
        {
            for (int i = 0; i < bestScores.Count; i++)
            {
                if (score > bestScores[i].bestScore)
                {
                    bestScores.Insert(i, new BestScoreData() { bestScoreName = name, bestScore = score });
                    return;
                }
            }
        }
        bestScores.Add(new BestScoreData() { bestScoreName = name, bestScore = score });

    }

    [System.Serializable]
    class SaveData
    {
        public List<BestScoreData> bestScores;
    }

    // Save best scorer and their score for future sessions
    public void SaveBestScores()
    {
        SaveData data = new SaveData();
        data.bestScores = bestScores;
        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    // Load best scorer and their score from previous save
    public void LoadBestScores()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestScores = data.bestScores;
        }
    }
}

