using UnityEngine;
using System.Collections;

public class Money : MonoBehaviour
{
    public RectTransform gemCounterUI;  // assigner le compteur UI dans l’inspector
    private float flyDuration = 0.5f;    // durée du vol vers le UI

    private float amplitude = 0.5f;
    private float speed = 2f;
    private float magnetismRadius = 2f;
    private float magnetismSpeed = 8f;

    private float startY;
    private Transform playerTransform;
    private bool isCollected = false;

    void Start()
    {
        startY = transform.position.y;
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (isCollected) return;

        // Magnétisme vers le joueur
        if (playerTransform != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
            if (distanceToPlayer < magnetismRadius)
            {
                Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;
                transform.position += (Vector3)directionToPlayer * magnetismSpeed * Time.deltaTime;
                return;
            }
        }

        // Rebond normal
        float newY = startY + Mathf.Sin(Time.time * speed) * amplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isCollected)
        {
            isCollected = true;

            // Convertit la position UI en world space
            Vector3 target = Camera.main.ScreenToWorldPoint(gemCounterUI.position);
            target.z = 0f; // garde le Z correct pour la 2D

            StartCoroutine(FlyToUI(target, collision.gameObject.GetComponent<PlayerInventory>()));
            gemCounterUI.GetComponent<UIBump>().DoBump();
        }
    }

    private IEnumerator FlyToUI(Vector3 targetPos, PlayerInventory playerInventory)
    {
        Vector3 startPos = transform.position;
        float elapsed = 0f;
        float baseScale = 0.15f;

        while (elapsed < flyDuration)
        {
            float t = elapsed / flyDuration;
            t = t * t * (3f - 2f * t); // smoothstep

            transform.position = Vector3.Lerp(startPos, targetPos, t);

            // Scale pop léger
            float scale = Mathf.Lerp(baseScale, baseScale * 1.2f, t);
            transform.localScale = Vector3.one * scale;

            // Rotation pendant le vol
            transform.Rotate(0f, 0f, 360f * Time.deltaTime);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
        transform.localScale = Vector3.one * baseScale;

        // Ajoute l’argent au joueur
        playerInventory.AddMoney();

        Destroy(gameObject);
    }
}
