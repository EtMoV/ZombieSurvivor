using UnityEngine;

public class GhostFade : MonoBehaviour
{
    private SpriteRenderer sr;
    private float fadeSpeed = 5f; // Alpha diminue à 5 par seconde

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Color c = sr.color;
        c.a -= fadeSpeed * Time.deltaTime;
        sr.color = c;

        if (c.a <= 0)
            Destroy(gameObject);
    }
}
