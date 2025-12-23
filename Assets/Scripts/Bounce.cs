using UnityEngine;

public class Bounce : MonoBehaviour
{
    public float amplitude; // hauteur du rebond
    public float speed;       // vitesse du rebond
    private float startY; // position de départ sur l'axe Y

    void Start()
    {
        // Enregistre la position de départ
        startY = transform.position.y;
    }

    void Update()
    {
        float newY = startY + Mathf.Sin(Time.time * speed) * amplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
