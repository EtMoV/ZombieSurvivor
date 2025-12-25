using UnityEngine;

public class FoodCan : MonoBehaviour
{
    private float amplitude = 0.5f; // hauteur du rebond
    private float speed = 2f;       // vitesse du rebond
    private float magnetismRadius = 2f; // rayon du magnétisme
    private float magnetismSpeed = 8f;   // vitesse d'attraction

    private float startY; // position de départ sur l'axe Y
    private Transform playerTransform;

    void Start()
    {
        // Enregistre la position de départ
        startY = transform.position.y;

        // Trouve le joueur
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        // Magnétisme vers le joueur
        if (playerTransform != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

            if (distanceToPlayer < magnetismRadius)
            {
                // Attire directement vers le joueur
                Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;
                transform.position += (Vector3)directionToPlayer * magnetismSpeed * Time.deltaTime;
                return; // Sort du Update pour ne pas appliquer le rebond
            }
        }

        // Si pas de magnétisme, applique le rebond normal
        float newY = startY + Mathf.Sin(Time.time * speed) * amplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerTransform.gameObject.GetComponent<PlayerInventory>().AddCanFood();
            Destroy(gameObject);
        }
    }
}
