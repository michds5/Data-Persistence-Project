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
        bestScoreText.text = $"Best Score : {NameScoreManager.instance.bestScoreName} : {NameScoreManager.instance.bestScore}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Player name input by player - need to test what happens with large names
    public void SubmitName(string name)
    {
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

}
