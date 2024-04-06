using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NameScoreManager : MonoBehaviour
{
    public static NameScoreManager instance;

    public string playerName;
    public string highName;
    public int highScore = 0;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        LoadHighScore();
    }

    [System.Serializable]
    class SaveData
    {
        public string highName;
        public int highScore;
    }

    public void SaveHighScore()
    {
        SaveData data = new SaveData();
        data.highName = highName;
        data.highScore = highScore;
        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highName = data.highName;
            highScore = data.highScore;
        }
    }
}
