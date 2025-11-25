using UnityEngine;

public class CameraAdjust2D : MonoBehaviour
{
    private float wearableSize = 7f;
    private float phoneSize = 15f;

    public bool isScenario = false;

    void Start()
    {
        Camera cam = GetComponent<Camera>();
        float aspect = (float)Screen.width / Screen.height;
        
        if (aspect < 1f) // téléphone
            cam.orthographicSize = phoneSize;
        else // petit écran carré → Wear OS
            cam.orthographicSize = wearableSize;

        if (isScenario)
        {
            cam.orthographicSize = 10f;
        }
    }
}
