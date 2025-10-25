using System.Collections;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10f;
    public GameObject bullet; // Bullet prefab to be instantiated
    public Transform firePoint; // Point from where the bullet will be fired

    public float fireRate = 0.003f; // Time between shots
    private float nextFire = 0f;   // Moment when the player can fire next

    // Slide-in variables
    public float startY = -8f;       // Below the screen
    public float targetY = -3.5f;      // Where it stops
    public float slideSpeed = 2f;
    private bool slidingIn = true;

    private Vector3 targetPos;


    private void Start()
    {
        // Place ship at start position below screen
        Vector3 pos = transform.position;
        pos.y = startY;
        transform.position = pos;

        targetPos = new Vector3(pos.x, targetY, pos.z);

        // Start sliding in
        StartCoroutine(SlideIn());
    }


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

       
        // Clamp position only if not sliding in
        if (!slidingIn)
        {
            ClampPosition();
        }
            
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Asteroid"))
        {
            Destroy(other.gameObject);

            // Trigger the flash effect
            StartCoroutine(FlashEffect());
        }

       
    }

    void ClampPosition()     {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -8.5f, 8.5f);
        pos.y = Mathf.Clamp(pos.y, -4.5f, 4.5f);
        transform.position = pos;
    }

    void Shoot()
        {
        if (Input.GetKey(KeyCode.Space) && Time.time >= nextFire)
        {
            Debug.Log("Shoot!");
            Instantiate(bullet, firePoint.position, Quaternion.identity);
            nextFire = Time.time + fireRate; // update next fire time

        }
    }

    /* Coroutines are special methods that can pause execution and return control
     * to Unity but then continue where they left off on the following frame.
     * 
     * IEnumerator is the return type for coroutines.
     */

    // Flash coroutine
    IEnumerator FlashEffect()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Color originalColor = sr.color;
        sr.color = Color.red;          
        yield return new WaitForSeconds(0.05f);  // duration of the flash
        sr.color = originalColor;
        yield return new WaitForSeconds(0.02f);
        sr.color = Color.red;
        yield return new WaitForSeconds(0.05f); 
        sr.color = originalColor;

    }

    // Slide-in coroutine
    IEnumerator SlideIn()
    {
        while (transform.position.y < targetPos.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, slideSpeed * Time.deltaTime);
            yield return null;
        }

        slidingIn = false; // enable clamping after slide-in
    }
}
