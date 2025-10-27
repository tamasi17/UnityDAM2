using UnityEngine;

public class Generator : MonoBehaviour
{

    public GameObject asteroid;

    public float spawnRateMin = 2f;
    public float spawnRateMax = 5f;
    private float screenWidth = 8f; // Approximate half-width of the screen in world units


    void Start()
    {
        Invoke("SpawnAsteroid", Random.Range(spawnRateMin, spawnRateMax));
    }

    private void Update()
    {
        // Gradually decrease spawn rates to increase difficulty
        spawnRateMin = Mathf.Max(0.2f, spawnRateMin - 1.5f * Time.deltaTime);
        spawnRateMax = Mathf.Max(0.5f, spawnRateMax - 3f * Time.deltaTime);
    }

    void SpawnAsteroid()
    {
        // Random horizontal position
        Vector3 spawnPos = new Vector3(Random.Range(-screenWidth, screenWidth), 6f, 0);
        
        // Instantiate the asteroid without modifying the original prefab
        GameObject newAsteroid = Instantiate(asteroid, spawnPos, Quaternion.identity);
        newAsteroid.GetComponent<Asteroid>().speed = Random.Range(2f, 7f);

        // Repeat spawning
        Invoke("SpawnAsteroid", Random.Range(spawnRateMin, spawnRateMax));
    }
}
