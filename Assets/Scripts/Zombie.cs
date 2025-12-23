using UnityEngine;

public class Zombie : MonoBehaviour
{
    public System.Action OnDeath;
    public LayerMask obstacleMask;
    private Rigidbody2D rb;
    private SpriteRenderer _spriteRenderer;
    private Transform player;
    private Vector2 knockbackVelocity;
    private float moveSpeed = 1.5f;
    private float knockbackDecay = 10f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    void FixedUpdate()
    {
        if (player == null || GetComponent<ZombieLife>().isDead || player.gameObject.GetComponent<PlayerLife>().isDead) return;

        Vector2 dirToPlayer = player.position - transform.position;
        bool canSeePlayer = !Physics2D.Raycast(transform.position, dirToPlayer.normalized,
                                               dirToPlayer.magnitude, obstacleMask);

        if (canSeePlayer)
        {
            // Fonce droit sur le joueur
            rb.linearVelocity = dirToPlayer.normalized * moveSpeed + knockbackVelocity;
            _spriteRenderer.flipX = player.position.x < transform.position.x;
        }

        knockbackVelocity = Vector2.Lerp(knockbackVelocity, Vector2.zero, knockbackDecay * Time.fixedDeltaTime);
    }

    public void ApplyKnockback(Vector2 direction, float force)
    {
        knockbackVelocity = direction.normalized * force;
    }
}
