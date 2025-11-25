using UnityEngine;

public class XP : MonoBehaviour
{
    public Inventory inventory;
    public float amplitude = 0.5f; // hauteur du rebond
    public float speed = 2f;       // vitesse du rebond
    private float startY; // position de départ sur l'axe Y

    void Start()
    {
        // Enregistre la position de départ
        startY = transform.position.y;
    }

    void Update()
    {
        // Calcule le décalage vertical
        float newY = startY + Mathf.Sin(Time.time * speed) * amplitude;

        // Applique le mouvement
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inventory.xp++; // Augmente l'xp de 1
            Destroy(gameObject);
        }
    }
}
