using UnityEngine;

public class Asteroid : MonoBehaviour
{

    public float speed = 3f;
    public float minSize = 0.5f;
    public float maxSize = 1.5f;


    void Start()
    {
        // Random size
        float size = Random.Range(minSize, maxSize);
        transform.localScale = new Vector3(size, size, 1);
    }

    
    void Update()
    {
        // Downward movement
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        // If it goes off screen, destroy it
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // If it collides with the player or a bullet, destroy both
        Debug.Log("Asteroid collided with: " + other.tag);

        if (other.CompareTag("Bullet"))
        {
            GameManager.instance.AddScore(); // add score
            Destroy(other.gameObject); // bullet
            Destroy(gameObject);       // asteroid
        }

        if (other.CompareTag("Player"))
        {
            GameManager.instance.LoseLife(); // subtract 1 life
            Destroy(gameObject);       // asteroid
        }
    }

}
