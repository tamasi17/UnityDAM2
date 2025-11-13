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
    public Button againButton;


    void Start()
    {
        // Hide the intro text at the start
        introText.gameObject.SetActive(false);

        // Assign button listeners
        startButton.onClick.AddListener(StartGame);
        exitButton.onClick.AddListener(ExitGame);
        againButton.onClick.AddListener(PlayAgain);
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
        yield return new WaitForSeconds(7f);
        SceneManager.LoadScene("Intro");
    }

    public void ExitGame()
    {
        // Exit the application
        Application.Quit();
        Debug.Log("Game Exited");
    }

    public void PlayAgain()
    {
        // Play again the game from the beginning
        SceneManager.LoadScene("MenuPrincipal");
        Debug.Log("Jugamos otra vez!");
    }
}
