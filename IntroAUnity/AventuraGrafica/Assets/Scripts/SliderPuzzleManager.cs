using System.Collections;
using System.Diagnostics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SliderPuzzleManager : MonoBehaviour
{

    
    public Canvas affirmationCanvas;
    
    public Slider sliderOne;
    public TMP_Text answerOne;
    
    public Slider sliderTwo;
    public TMP_Text answerTwo;
    
    public Slider sliderThree;
    public TMP_Text answerThree;

    public Button door;
    public TMP_Text doorText;
    private bool allCorrect = false;
    private string personality;

    public CanvasGroup cantSeeCanvas;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Start blind
        cantSeeCanvas.alpha = 1;
        affirmationCanvas.GetComponent<CanvasGroup>().alpha = 0;

        UnityEngine.Debug.Log("Personality: " + GameData.Instance.playerPersonality);

        sliderOne.onValueChanged.AddListener(delegate { SetAnswerOne(); });
        sliderTwo.onValueChanged.AddListener(delegate { SetAnswerTwo(); });
        sliderThree.onValueChanged.AddListener(delegate { SetAnswerThree(); });

        sliderOne.onValueChanged.AddListener(delegate { SetAnswerOne(); CheckAnswers(); });
        sliderTwo.onValueChanged.AddListener(delegate { SetAnswerTwo(); CheckAnswers(); });
        sliderThree.onValueChanged.AddListener(delegate { SetAnswerThree(); CheckAnswers(); });

        door.onClick.AddListener(TryOpeningDoor);

    }

    public void CheckAnswers()
    {
        cantSeeCanvas.alpha = 0;

        personality = GameData.Instance.playerPersonality;
        UnityEngine.Debug.Log("Checking answers for personality: " + personality);

        if (personality == "Optimistic")
        {
            CheckOptimisticAnswers();
        }
        else if (personality == "Gloomy")
        {
            CheckGloomyAnswers();
        }
        else if (personality == "Detached")
        {
            CheckDetachedAnswers();
        }
        
    }

    private void CheckOptimisticAnswers()
    {
        UnityEngine.Debug.Log("Checking optimistic answers: " + answerOne.text);

        float valueOne = sliderOne.value;
        float valueTwo = sliderTwo.value;
        float valueThree = sliderThree.value;

        UnityEngine.Debug.Log($"Slider values: {valueOne}, {valueTwo}, {valueThree}");

        if (valueOne == 1 && valueTwo == 2 && valueThree == 1)
        {
            UnityEngine.Debug.Log("All optimistic answers correct.");
            allCorrect = true;
            door.gameObject.SetActive(true);
        }
        else
        {
            allCorrect = false;
        }
    }

    private void CheckGloomyAnswers()
    {
        UnityEngine.Debug.Log("Checking gloomy answers: "+ answerOne.text);

        float valueOne = sliderOne.value;
        float valueTwo = sliderTwo.value;
        float valueThree = sliderThree.value;

        UnityEngine.Debug.Log($"Slider values: {valueOne}, {valueTwo}, {valueThree}");

        if (valueOne == 2 && valueTwo == 1 && valueThree == 2)
        {
            UnityEngine.Debug.Log("All gloomy answers correct.");
            allCorrect = true;
            door.gameObject.SetActive(true);
        }
        else
        {
            allCorrect = false;
        }
    }

    private void CheckDetachedAnswers()
    {
        UnityEngine.Debug.Log("Checking detached answers: "+ answerOne.text);
        
        float valueOne = sliderOne.value;
        float valueTwo = sliderTwo.value;
        float valueThree = sliderThree.value;

        UnityEngine.Debug.Log($"Slider values: {valueOne}, {valueTwo}, {valueThree}");


        if (valueOne == 3 && valueTwo == 3 && valueThree == 3)
        {
            UnityEngine.Debug.Log("All detached answers correct.");
            allCorrect = true;
            door.gameObject.SetActive(true);
            
        }
        else
        {   
            allCorrect = false;
        }
    }

    public void TryOpeningDoor()
    {
        
            if (allCorrect)
            {
                UnityEngine.Debug.Log("Correct Answer! Door Unlocked.");
                
                doorText.text = "Door is open now.";
                StartCoroutine(LoadNextSceneAfterDelay());

                affirmationCanvas.GetComponent<CanvasGroup>().alpha = 0;

        }
            else
            {
                UnityEngine.Debug.Log("The door is still locked.");
                
                doorText.text = "Door is still locked.";
                
                StartCoroutine(ShakeButton());
                StartCoroutine(ResetButtonVisual());
            }
    }

    public void SetAnswerOne()
    {
        if (sliderOne != null && sliderOne.value == 1)
            answerOne.text = "- for the better.";
        if (sliderOne != null && sliderOne.value == 2)
            answerOne.text = "- too fast.";
        if (sliderOne != null && sliderOne.value == 3)
            answerOne.text = ", as expected.";
    }

    public void SetAnswerTwo()
    {
        if (sliderTwo != null && sliderTwo.value == 1)
            answerTwo.text = "- probably worse.";
        if (sliderTwo != null && sliderTwo.value == 2)
            answerTwo.text = "- something wonderful!";
        if (sliderTwo != null && sliderTwo.value == 3)
            answerTwo.text = "- but who cares?";
    }

    public void SetAnswerThree()
    {
        if (sliderThree != null && sliderThree.value == 1)
            answerThree.text = ", if I try.";
        if (sliderThree != null && sliderThree.value == 2)
            answerThree.text = ", barely.";
        if (sliderThree != null && sliderThree.value == 3)
            answerThree.text = ", I guess.";
    }

    // Coroutine to reset the button's visual state
    private IEnumerator ResetButtonVisual()
    {
        // Temporarily disable & re-enable the button to refresh highlight colors
        yield return new WaitForSeconds(0.07f);
        door.interactable = false;
        yield return new WaitForSeconds(0.05f);
        door.interactable = true;
    }

    private IEnumerator ShakeButton()
    {
        Vector3 originalScale = door.transform.localScale;

        // quick "pop" up
        door.transform.localScale = originalScale * 1.03f;
        yield return new WaitForSeconds(0.05f);

        // small shake back and forth
        for (int i = 0; i < 2; i++)
        {
            door.transform.localScale = originalScale * 0.99f;
            yield return new WaitForSeconds(0.03f);
            door.transform.localScale = originalScale * 1.03f;
            yield return new WaitForSeconds(0.03f);
        }

        // return to normal
        door.transform.localScale = originalScale;
    }

    private IEnumerator LoadNextSceneAfterDelay()
    {
        yield return new WaitForSeconds(1f);

        personality = GameData.Instance.playerPersonality;
        UnityEngine.Debug.Log("Loading scene for: " + personality);

        if (personality == "Optimistic")
        {
            SceneManager.LoadScene("OptimisticEnding");
        }
        else if (personality == "Gloomy")
        {
            SceneManager.LoadScene("GloomyEnding");
        }
        else if (personality == "Detached")
        {
            SceneManager.LoadScene("DetachedEnding");
        }

        
    }

}
