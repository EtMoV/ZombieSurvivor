using UnityEngine;

public class PlayerV2 : MonoBehaviour
{
    [Header("Movement")]
    public float speed;
    public float jumpForce;

    private Vector2 moveDirection;
    private Rigidbody2D rb;

    [Header("Animation")]
    public Animator animator;

    [Header("Health")]
    private int maxHealth;
    public int currentHealth;

    [Header("Damage Over Time")]
    public int damagePerTick;
    public float damageInterval;

    [Header("Shooting")]
    public float shootInterval;
    public int damagePerShot;
    private float shootTimer;

    [Header("Detection Zone")]
    public Transform detectionZone; // enfant avec Collider2D (IsTrigger)

    public bool canShoot = true;
    public bool isDead = false;

    [Header("Ground")]
    public LayerMask groundLayer;
    private bool isGrounded;

    void Start()
    {
        maxHealth = 10;
        speed = 5f;
        jumpForce = 6f;
        damagePerTick = 5;
        damageInterval = 1f;
        shootInterval = 5f;
        damagePerShot = 10;

        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;

        if (animator == null)
            animator = GetComponent<Animator>();

    }

    void Update()
    {
        // ✅ Détection du sol via IsTouchingLayers
        isGrounded = rb.IsTouchingLayers(groundLayer);

        HandleMovement();
        HandleFlip();

        if (shootTimer > 0f)
            shootTimer -= Time.deltaTime;
    }

    // ---------------- Movement ----------------
    void HandleMovement()
    {
        rb.linearVelocity = new Vector2(moveDirection.x * speed, rb.linearVelocity.y);
        canShoot = Mathf.Abs(rb.linearVelocity.x) < 0.05f;
    }

    // ---------------- Flip + Detection Zone ----------------
    void HandleFlip()
    {
        if (rb.linearVelocity.x > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
            FlipDetectionZone(1);
        }
        else if (rb.linearVelocity.x < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            FlipDetectionZone(-1);
        }
    }

    void FlipDetectionZone(int dir)
    {
        if (detectionZone == null) return;

        Vector3 pos = detectionZone.localPosition;
        pos.x = Mathf.Abs(pos.x) * dir;
        detectionZone.localPosition = pos;
    }

    // =================================================
    // ======= MÉTHODES PUBLIQUES POUR LES BOUTONS ======
    // =================================================

    public void MoveLeft()
    {
        if (!isDead)
        {
            moveDirection = new Vector2(-1, 0);
            animator.Play("Player-walk");
        }
    }

    public void MoveRight()
    {
        if (!isDead)
        {
            moveDirection = new Vector2(1, 0);
            animator.Play("Player-walk");
        }
    }

    public void MoveUp()
    {
        if (!isDead)
        {
            if (!isGrounded) return; // saut uniquement si sol

            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            canShoot = false;
            animator.Play("Player-jump");
        }
    }

    public void StopMove()
    {

        moveDirection = Vector2.zero;
        canShoot = true;
        if (!isDead)
            animator.Play("Player-idle");
    }

    // MEthode de prise de degats
    public void TakeDamage(int damage)
    {
        canShoot = false;
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        canShoot = false;
        isDead = true;
        // Bloque totalement le joueur
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        // Optionnel : désactiver le mouvement et le tir
        moveDirection = Vector2.zero;
        animator.Play("Player-dead");
    }
}
