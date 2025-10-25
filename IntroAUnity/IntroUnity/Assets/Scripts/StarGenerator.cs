using UnityEngine;

public class StarGenerator : MonoBehaviour
{
    public GameObject starPrefab;

    public float spawnRateMin = 9f;
    public float spawnRateMax = 15f;
    private float screenWidth = 8f; // Approximate half-width of the screen in world units


    void Start()
    {
        Invoke("SpawnStar", Random.Range(spawnRateMin, spawnRateMax));
    }

    private void Update()
    {
        // Gradually decrease spawn rates to increase difficulty
        spawnRateMin = Mathf.Max(0.5f, spawnRateMin - 1.2f * Time.deltaTime);
        spawnRateMax = Mathf.Max(1f, spawnRateMax - 1.2f * Time.deltaTime);
    }

    void SpawnStar()
    {
        // Random horizontal position
        Vector3 spawnPos = new Vector3(Random.Range(-screenWidth, screenWidth), 6f, 0);

        // Instantiate the asteroid without modifying the original prefab
        GameObject newStar = Instantiate(starPrefab, spawnPos, Quaternion.identity);
        newStar.GetComponent<Star>().speed = Random.Range(1f, 3f);

        // Repeat spawning
        Invoke("SpawnStar", Random.Range(spawnRateMin, spawnRateMax));
    }
}
