using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 9f;
    public TMP_Text score;  // Text component to display the score
    public GameObject bullet; // Bullet prefab to be instantiated
    public Transform firePoint; // Point from where the bullet will be fired

    public float fireRate = 0.003f; // Time between shots
    private float nextFire = 0f;   // Moment when the player can fire next


    void Update()
    {
        MovePlayer();
        Shoot();

    }

    void MovePlayer()
    {
        float x = 0f, y = 0f;

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            y += 1;
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            y -= 1;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            x -= 1;
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            x += 1;

        Vector2 move = new Vector2(x, y) * speed * Time.deltaTime;
        transform.Translate(move);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Asteroid"))
        {
            Destroy(other.gameObject);
        }
    }

    void Shoot()
        {
        if (Input.GetKey(KeyCode.Space) && Time.time >= nextFire)
        {
            Debug.Log("Shoot!");
            Instantiate(bullet, firePoint.position, Quaternion.identity);
            nextFire = Time.time + fireRate; // actualiza el siguiente disparo permitido

        }
    }
}
