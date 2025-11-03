using UnityEngine;
using UnityEngine.UI;

public class EndingManager : MonoBehaviour
{
    public Button exitButton;
    public Button creditsButton;

    void Start()
    {
        // Assign button listener
        exitButton.onClick.AddListener(ExitGame);
        creditsButton.onClick.AddListener(GoToCredits);
    }
    public void ExitGame()
    {
        // Exit the application
        Application.Quit();
        Debug.Log("Game Exited");
    }

    public void GoToCredits()
    {
        // Load the credits scene
        Debug.Log("Loading credits");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Credits");
    }
}
