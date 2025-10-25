using System.Collections;
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

        Destroy(GameObject.FindWithTag("Player"));

        SpeedUpAsteroids();
        

        // Show panel
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }


        // Wait 0.5–1 sec before freezing
        Invoke(nameof(FreezeGame), 4f);

    }

    // Make remaining asteroids go crazy fast
    void SpeedUpAsteroids()
    {
        
        // Asteroid array, GameObject.FindObjectsByType requires Unity 2023.1 or newer
        Asteroid[] asteroids = GameObject.FindObjectsByType<Asteroid>(FindObjectsSortMode.None);

        foreach (Asteroid a in asteroids)
        {
            a.speed *= 8f; // multiply current speed by 10
        }
    }

    void FreezeGame()
    {
        Time.timeScale = 0f; // freeze
    }

}
