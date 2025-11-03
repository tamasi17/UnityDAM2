using UnityEngine;

public class ResolutionManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetResolution(1920, 1080, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            SetResolution(1920, 1080, true);
        if (Input.GetKeyDown(KeyCode.F2))
            SetResolution(2560, 1440, true);
        if (Input.GetKeyDown(KeyCode.F3))
            SetResolution(3840, 1440, true);
        if (Input.GetKeyDown(KeyCode.F4))
            SetResolution(2880, 1800, true);
    }

    public void SetResolution(int width, int height, bool fullscreen)
    {
        Screen.SetResolution(width, height, fullscreen);
        Debug.Log($"Resolución cambiada a: {width}x{height}");
    }


}
