using System.Collections;
using UnityEngine;

public class ZombieHealthV2 : MonoBehaviour
{
    public int maxHealth = 50;
    private int currentHealth;

    [Header("Knockback")]
    public float knockbackForce = 1f; // distance du knockback
    public float freezeDuration = 1f; // durée du freeze en secondes

    private Rigidbody2D rb;
    private PlayerV2 player;
    private ZombieFollowPlayerXV2 movementScript; // type exact du script de mouvement
    private bool isFrozen = false;

    public Animator animator;

    public bool isAttacking;

    void Start()
    {
        currentHealth = maxHealth;
        isAttacking = false;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
        }

        // Récupère le player
        player = Object.FindFirstObjectByType<PlayerV2>();
        if (player == null)
            Debug.LogWarning("Aucun PlayerV2 trouvé dans la scène !");

        // Récupère le script de mouvement du zombie
        movementScript = GetComponent<ZombieFollowPlayerXV2>();
        if (movementScript == null)
            Debug.LogWarning("Aucun script de mouvement ZombieFollowPlayerXV2 trouvé !");
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
            animator.Play("Zombie-dead");
            Die();
        }
        else
        {
            animator.Play("Zombie-hurt");
            ApplyKnockback();
            if (!isFrozen && movementScript != null)
                StartCoroutine(FreezeCoroutine());
        }
    }

    void ApplyKnockback()
    {
        float dirX = Mathf.Sign(transform.position.x - player.transform.position.x);
        Vector2 knockDir = new Vector2(dirX * knockbackForce, 0);
        rb.position += knockDir; // déplacement instantané sur X
    }

    IEnumerator FreezeCoroutine()
    {
        isFrozen = true;

        // Désactive le script de mouvement
        movementScript.enabled = false;

        yield return new WaitForSeconds(freezeDuration);

        // Réactive le script
        movementScript.enabled = true;
        isFrozen = false;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
