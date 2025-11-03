using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    // UI Buttons, assigned via the Unity Inspector
    [Header("UI Elements")]
    public TMP_Text title;
    public TMP_Text introText;
    public Button startButton;
    public Button exitButton;


    void Start()
    {
        // Hide the intro text at the start
        introText.gameObject.SetActive(false);

        // Assign button listeners
        startButton.onClick.AddListener(StartGame);
        exitButton.onClick.AddListener(ExitGame);
    }

    public void StartGame()
    {
        // Hide title and buttons
        title.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);

        // Show intro text
        introText.gameObject.SetActive(true);

        // Load the main game scene
        StartCoroutine(LoadNextSceneAfterDelay());
        
    }

    private IEnumerator LoadNextSceneAfterDelay()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Intro");
    }

    public void ExitGame()
    {
        // Exit the application
        Application.Quit();
        Debug.Log("Game Exited");
    }
}
