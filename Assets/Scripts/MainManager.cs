using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text bestScoreText;
    public GameObject gameOverDisplay;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
        bestScoreText.text = $"Best Score : {NameScoreManager.instance.bestScoreName} : {NameScoreManager.instance.bestScore}";
        ScoreText.text = $"Score - {NameScoreManager.instance.playerName} : 0";
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else if (Input.GetKeyDown(KeyCode.M))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score - {NameScoreManager.instance.playerName} : {m_Points}";
    }

    public void GameOver()
    {
        // Determine if new score beats the previous best score
        if (NameScoreManager.instance.bestScore < m_Points)
        {
            NameScoreManager.instance.bestScore = m_Points;
            NameScoreManager.instance.bestScoreName = NameScoreManager.instance.playerName;
        }
        NameScoreManager.instance.SaveBestScore();
        m_GameOver = true;
        bestScoreText.text = $"Best Score : {NameScoreManager.instance.bestScoreName} : {NameScoreManager.instance.bestScore}";
        gameOverDisplay.SetActive(true);
    }
}
