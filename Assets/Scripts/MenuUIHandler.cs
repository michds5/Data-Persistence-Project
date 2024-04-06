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
        bestScoreText.text = $"Best Score : {NameScoreManager.instance.highName} : {NameScoreManager.instance.highScore}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SubmitName(string name)
    {
       NameScoreManager.instance.playerName = name;
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

}
