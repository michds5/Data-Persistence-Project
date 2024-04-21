using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    public TextMeshProUGUI bestScoreText;

    // Start is called before the first frame update
    void Start()
    {
        if (NameScoreManager.instance.topScores.Count > 0)
        {
            bestScoreText.text = $"Best Score : " +
                $"{NameScoreManager.instance.topScores[0].top10ScoreName} : " +
                $"{NameScoreManager.instance.topScores[0].top10Score}";
        }
        else
        {
            bestScoreText.text = $"Best Score : 0";
        }
    }

    // Player name input by player (max string length of 10)
    public void SubmitName(string name)
    {
        int maxNameLength = 10;

        if (name.Length > maxNameLength)
        {
            name = name.Substring(0, maxNameLength);
        }
        NameScoreManager.instance.playerName = name;
    }

    // Referenced by menu start button
    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    // Referenced by menu quit button
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    // Referenced by Top 10 Scores button
    public void GoToTopScores()
    {
        SceneManager.LoadScene(2);
    }

}