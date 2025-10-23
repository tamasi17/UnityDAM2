using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance; // singleton pattern

    public int score;
    public int lives = 3;
     
    public TMP_Text scoreText;
    public TMP_Text livesText;
    public GameObject gameOverPanel;

    public GameObject playerGameObject;


    void Awake()
    {
        // Singleton pattern
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }


    void Start()
    {
        UpdateUI();
        gameOverPanel.SetActive(false);
    }

    public void AddScore()
    {
        score += 10;
        UpdateUI();
    }

    public void LoseLife()
    {
        lives--;
        UpdateUI();

        if (lives <= 0)
        {
            GameOver();
        }
    }

    void UpdateUI()
    {
        if (scoreText != null) scoreText.text = "Score: " + score;
        if (livesText != null) livesText.text = "Lives: " + lives;
    }

    void GameOver()
    {
        Debug.Log("Game Over!");
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        Time.timeScale = 0; // freeze game
        
    }

}
