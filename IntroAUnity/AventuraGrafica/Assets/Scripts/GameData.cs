using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance; // Singleton reference

    public string playerName;
    public string playerPersonality;

    void Awake()
    {
        // Singleton pattern — keeps this object when scenes change
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

