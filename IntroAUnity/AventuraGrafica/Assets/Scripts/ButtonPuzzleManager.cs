using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonPuzzleManager : MonoBehaviour
{

    public Toggle optimisticToggle;
    public Toggle gloomyToggle;
    public Toggle detachedToggle;

    private bool correctAnswer = false;

    public Button nextDoor;
    public TMP_Text doorText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nextDoor.gameObject.SetActive(false);
        doorText.text = "Have all these years been worth something?";

        Debug.Log("Personality: "+ GameData.Instance.playerPersonality);

        optimisticToggle.onValueChanged.AddListener(delegate { AnswerIsCorrect(); });
        gloomyToggle.onValueChanged.AddListener(delegate { AnswerIsCorrect(); });
        detachedToggle.onValueChanged.AddListener(delegate { AnswerIsCorrect(); });

        AnswerIsCorrect();

        nextDoor.onClick.AddListener(OnDoorButtonClicked);

    }

    public void AnswerIsCorrect()
    {

        // Read personality directly from GameData
        string playerPersonality = GameData.Instance.playerPersonality;

        Debug.Log("Checking personality and toggles: " + GameData.Instance.playerPersonality 
            + "\n Optimistic answer: "+ optimisticToggle.isOn
            + "\n Gloomy answer: " + gloomyToggle.isOn
            + "\n Detached answer: " + detachedToggle.isOn);

        if (playerPersonality == "Optimistic")
        {
            if (optimisticToggle.isOn)
            {
                correctAnswer = true;
                PuzzleCompleted();
            } else
            {
                correctAnswer = false;
                nextDoor.gameObject.SetActive(false);

            }
        }
        else if (playerPersonality == "Gloomy")
        {
            if (gloomyToggle.isOn)
            {
                correctAnswer = true;
                PuzzleCompleted();
            }
            else
            {
                correctAnswer = false;
                nextDoor.gameObject.SetActive(false);

            }
        }
        else if (playerPersonality == "Detached")
        {
            if (detachedToggle.isOn)
            {
                correctAnswer = true;
                PuzzleCompleted();
            }
            else
            {
                correctAnswer = false;
                nextDoor.gameObject.SetActive(false);

            }
        }
    }

    public void PuzzleCompleted()
    {
        Debug.Log("Checking answer, correctAnswer is: "+ correctAnswer);

        if (correctAnswer)
        {
            Debug.Log("Correct Answer! Door is now visible.");
            nextDoor.gameObject.SetActive(true);

        }
        else
        {
            Debug.Log("Incorrect Answer. Try Again.");
        }
    }

    public void OnDoorButtonClicked()
    {
        Debug.Log("Door button clicked!");
        doorText.text = "I can open this door too.";
        
        nextDoor.gameObject.SetActive(false);
        Debug.Log("Going through!");
       
        StartCoroutine(LoadNextSceneAfterDelay());

    }

    private IEnumerator LoadNextSceneAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Sliders");
    }
}
