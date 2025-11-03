using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour
{
    // Key to press
    public KeyCode returnKey = KeyCode.Escape;

    void Update()
    {
        if (Input.GetKeyDown(returnKey))
        {
            SceneManager.LoadScene("MenuPrincipal"); 
        }
    }
}