using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{

    [SerializeField] public Button doorButton;
    [SerializeField] public TMP_Text doorButtonText;
    [SerializeField] public TMP_Text status;
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TMP_Dropdown personalityDropdown;

    private int doorCounter = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Hide the door button at first
        doorButton.gameObject.SetActive(false);
        doorButtonText.text = "Oh. That door again.";

        // Only way I found to hook the listeners, no Inspector or lambdas.
        nameInput.onValueChanged.AddListener(delegate { CheckUnlockConditions(); });
        nameInput.onValueChanged.AddListener(delegate { UpdateStatus(); });

        personalityDropdown.onValueChanged.AddListener(delegate { CheckUnlockConditions(); });
        personalityDropdown.onValueChanged.AddListener(delegate { UpdateStatus(); });


    }

    public void UpdateStatus()
    {
        string name = nameInput.text;
        string personality = personalityDropdown.options[personalityDropdown.value].text;

        status.text = $"{name}: {personality}";

        // Save the data globally
        if (GameData.Instance != null)
        {
            GameData.Instance.playerName = name;
            GameData.Instance.playerPersonality = personality;
        }
    }

    public void CheckUnlockConditions()
    {
        Debug.Log($"Checking unlock conditions: name = '{nameInput.text}', dropdown = {personalityDropdown.value}");

        bool hasName = !string.IsNullOrWhiteSpace(nameInput.text);
        bool hasPersonality = personalityDropdown.value >= 0; // Ensure a personality is selected

        Debug.Log($"Checking unlock: hasName={hasName}, hasPersonality={hasPersonality}");

        // If both conditions are true, show the button
        if (hasName && hasPersonality && !doorButton.gameObject.activeSelf)
        {
            doorButton.gameObject.SetActive(true);
            Debug.Log("Door button is now visible!");
        }
    }

    public void OnDoorButtonClicked()
    {  

        Debug.Log("Door button clicked!");

        if (doorCounter == 0)
        {
            doorButtonText.text = "I have seen this door before.";
            Debug.Log("Door seems locked");

        }
        else if (doorCounter == 1 || doorCounter == 2)
        {
            doorButtonText.text = "The door is still locked.";
            Debug.Log("The door is still locked.");
            StartCoroutine(ShakeButton());
        }
        else if (doorCounter >= 3)
        {
            doorButtonText.text = "The door is now open!";
            Debug.Log("The door is now open!");

            // Load next scene
            SceneManager.LoadScene("Botones");
            return;
        }

        doorCounter++;

        // Reset button’s color/visual state
        StartCoroutine(ResetButtonVisual());
    }

    // Coroutine to reset the button's visual state
    private IEnumerator ResetButtonVisual()
    {
        // Temporarily disable & re-enable the button to refresh highlight colors
        doorButton.interactable = false;
        yield return new WaitForSeconds(0.05f);
        doorButton.interactable = true;
    }

    private IEnumerator ShakeButton()
    {
        Vector3 originalScale = doorButton.transform.localScale;

        // quick "pop" up
        doorButton.transform.localScale = originalScale * 1.03f;
        yield return new WaitForSeconds(0.05f);

        // small shake back and forth
        for (int i = 0; i < 2; i++)
        {
            doorButton.transform.localScale = originalScale * 0.99f;
            yield return new WaitForSeconds(0.03f);
            doorButton.transform.localScale = originalScale * 1.03f;
            yield return new WaitForSeconds(0.03f);
        }

        // return to normal
        doorButton.transform.localScale = originalScale;
    }

}
