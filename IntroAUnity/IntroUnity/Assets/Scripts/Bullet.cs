using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 15f;

    
    void Start()
    {
        
    }

    void Update()
    {

        // Move the bullet upwards
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        // If it goes off screen, destroy it
        if (transform.position.y > 6f)
        {
            Destroy(gameObject);
        }
    }
}
