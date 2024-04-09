using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;



public class BestScoresUIHandler : MonoBehaviour
{
    public TextMeshProUGUI[] bestScoreNamesText; 
    public TextMeshProUGUI[] bestScoresText;

    // Start is called before the first frame update
    void Start()
    {
        PrintScores();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }


    public void ResetScores()
    {
        NameScoreManager.instance.bestScores.Clear();
        NameScoreManager.instance.SaveBestScores();

        for (int i = 0; i < bestScoreNamesText.Length; i++)
        {
            bestScoreNamesText[i].text = "";
            bestScoresText[i].text = "";
        }
    }

    public void PrintScores()
    {
        for(int i = 0; i < NameScoreManager.instance.bestScores.Count; i++)
        {
            bestScoreNamesText[i].text = NameScoreManager.instance.bestScores[i].bestScoreName;
            bestScoresText[i].text = $"{NameScoreManager.instance.bestScores[i].bestScore}";
        }
    }
}
