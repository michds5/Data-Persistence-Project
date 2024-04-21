using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;



public class TopScoresUIHandler : MonoBehaviour
{
    public GameObject resetTopScoresPopup;
    public TextMeshProUGUI[] topScoreNamesText;
    public TextMeshProUGUI[] topScoresText;

    // Start is called before the first frame update
    void Start()
    {
        PrintScores();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ShowResetTopScoresPopup()
    {
        resetTopScoresPopup.SetActive(true);
    }

    public void HideResetTopScoresPopup()
    {
        resetTopScoresPopup.SetActive(false);
    }

    public void ResetScores()
    {
        NameScoreManager.instance.topScores.Clear();
        NameScoreManager.instance.SaveTopScores();

        for (int i = 0; i < topScoreNamesText.Length; i++)
        {
            topScoreNamesText[i].text = "";
            topScoresText[i].text = "";
        }
        resetTopScoresPopup.SetActive(false);
    }

    public void PrintScores()
    {
        for (int i = 0; i < NameScoreManager.instance.topScores.Count; i++)
        {
            topScoreNamesText[i].text = $"{i + 1}.\t{NameScoreManager.instance.topScores[i].top10ScoreName}";
            topScoresText[i].text = $"{NameScoreManager.instance.topScores[i].top10Score}";
        }
    }
}