using UnityEngine;

public class SlowMo : MonoBehaviour
{
    public float zoomSpeed = 0.01f;  // How fast it zooms in
    public float maxScale = 1.2f;    // Max size relative to original

    private Vector3 initialScale;

    void Start()
    {
        initialScale = transform.localScale;
    }

    void Update()
    {
        if (transform.localScale.x < initialScale.x * maxScale)
        {
            transform.localScale += Vector3.one * zoomSpeed * Time.deltaTime;
        }
    }
}
