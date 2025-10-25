using UnityEngine;

public class Star : MonoBehaviour
{
    public float speed = 1f;

    void Update()
    {
        // Downward movement, Space.World to avoid rotation affecting direction
        transform.Translate(Vector2.down * speed * Time.deltaTime, Space.World);

   

        // If it goes off screen, destroy it
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }
}
