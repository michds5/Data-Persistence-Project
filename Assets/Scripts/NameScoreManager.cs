using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NameScoreManager : MonoBehaviour
{
    public static NameScoreManager instance;

    public string playerName;
    public string bestScoreName;
    public int bestScore = 0;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        LoadBestScore();
    }

    [System.Serializable]
    class SaveData
    {
        public string bestScoreName;
        public int bestScore;
    }

    // Save best scorer and their score for future sessions
    public void SaveBestScore()
    {
        SaveData data = new SaveData();
        data.bestScoreName = bestScoreName;
        data.bestScore = bestScore;
        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    // Load best scorer and their score from previous save
    public void LoadBestScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestScoreName = data.bestScoreName;
            bestScore = data.bestScore;
        }
    }
}
