using TMPro;
using UnityEngine;

public class DisplayPlayerInfo : MonoBehaviour
{
    private TMP_Text playerInfo;

    void Awake()
    {
        // Automatically grab the TMP_Text on this same object
        playerInfo = GetComponent<TMP_Text>();
    }

    void Start()
    {
        if (GameData.Instance != null)
        {
            playerInfo.text = $"{GameData.Instance.playerName}: {GameData.Instance.playerPersonality}";
        }
    }


}
