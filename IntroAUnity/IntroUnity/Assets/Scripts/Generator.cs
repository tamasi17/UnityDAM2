using UnityEngine;

public class Generator : MonoBehaviour
{

    public GameObject asteroid;

    public float spawnRateMin = 1f;
    public float spawnRateMax = 3f;
    //private float nextSpawn = 0f;

    private float screenWidth = 8f; // Approximate half-width of the screen in world units


    void Start()
    {
        Invoke("SpawnAsteroid", Random.Range(spawnRateMin, spawnRateMax));
    }

    void SpawnAsteroid()
    {
        // Random horizontal position
        Vector3 spawnPos = new Vector3(Random.Range(-screenWidth, screenWidth), 6f, 0);
        
        // Instantiate the asteroid without modifying the original prefab
        GameObject newAsteroid = Instantiate(asteroid, spawnPos, Quaternion.identity);
        newAsteroid.GetComponent<Asteroid>().speed = Random.Range(2f, 5f);

        // Random speed
        asteroid.GetComponent<Asteroid>().speed = Random.Range(2f, 5f);

        // Repeat spawning
        Invoke("SpawnAsteroid", Random.Range(spawnRateMin, spawnRateMax));
    }
}
