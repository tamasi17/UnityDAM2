using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonPuzzleManager : MonoBehaviour
{

    public Toggle goodnessToggle;
    public Toggle potentialToggle;
    public Toggle dreamsToggle;
    public Toggle weaknessToggle;
    public Toggle mistakesToggle;
    public Toggle fearsToggle;
    public Toggle patternsToggle;
    public Toggle habitsToggle;
    public Toggle natureToggle;

    private bool correctAnswer = false;

    public Button nextDoor;
    public TMP_Text doorText;


    void Start()
    {
        nextDoor.gameObject.SetActive(false);
        doorText.text = "Have all these years been worth something?";

        Debug.Log("Personality: "+ GameData.Instance.playerPersonality);

        goodnessToggle.onValueChanged.AddListener(delegate { AnswerIsCorrect(); });
        potentialToggle.onValueChanged.AddListener(delegate { AnswerIsCorrect(); });
        dreamsToggle.onValueChanged.AddListener(delegate { AnswerIsCorrect(); });

        weaknessToggle.onValueChanged.AddListener(delegate { AnswerIsCorrect(); });
        mistakesToggle.onValueChanged.AddListener(delegate { AnswerIsCorrect(); });
        fearsToggle.onValueChanged.AddListener(delegate { AnswerIsCorrect(); });
        
        patternsToggle.onValueChanged.AddListener(delegate { AnswerIsCorrect(); });
        habitsToggle.onValueChanged.AddListener(delegate { AnswerIsCorrect(); });
        natureToggle.onValueChanged.AddListener(delegate { AnswerIsCorrect(); });

        AnswerIsCorrect();

        nextDoor.onClick.AddListener(OnDoorButtonClicked);

    }

    public void AnswerIsCorrect()
    {

        // Read personality directly from GameData
        string playerPersonality = GameData.Instance.playerPersonality;

        Debug.Log("Checking personality and toggles: " + GameData.Instance.playerPersonality 
            + "\n Optimistic answer: "+ goodnessToggle.isOn
            + "\n Gloomy answer: " + weaknessToggle.isOn
            + "\n Detached answer: " + patternsToggle.isOn);

        if (playerPersonality == "Optimistic")
        {
            if (goodnessToggle.isOn && potentialToggle.isOn && dreamsToggle.isOn)
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
            if (weaknessToggle.isOn && mistakesToggle.isOn && fearsToggle.isOn)
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
            if (patternsToggle.isOn && habitsToggle.isOn && natureToggle.isOn)
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
