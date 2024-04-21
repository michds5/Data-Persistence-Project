using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text bestScoreText;
    public TextMeshProUGUI gameOverText;
    public GameObject gameOverPopup;

    private bool m_Started = false;
    private int m_Points;

    private bool m_GameOver = false;
    private int maxPoints = 96;

    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
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
        ScoreText.text = $"Score : {NameScoreManager.instance.playerName} : 0";
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
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {NameScoreManager.instance.playerName} : {m_Points}";
    }

    public void GameOver()
    {
        NameScoreManager.instance.AddScore(NameScoreManager.instance.playerName, m_Points);
        NameScoreManager.instance.SaveTopScores();
        m_GameOver = true;
        bestScoreText.text = $"Best Score : " +
            $"{NameScoreManager.instance.topScores[0].top10ScoreName} : " +
            $"{NameScoreManager.instance.topScores[0].top10Score}";

        if (m_Points >= maxPoints)
        {
            gameOverText.text = "YOU WIN!";
        }
        else
        {
            gameOverText.text = "GAME OVER";
        }

        gameOverPopup.SetActive(true);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
