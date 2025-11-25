using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Mouvement")]
    public float moveSpeed = 7f;
    public float stopThreshold = 0.05f;
    private bool _useJoystick = false; // Si vrai → on utilise le joystick, sinon le tap

    [Header("Joystick (si utilisé)")]
    public GameObject joystickUiGo;
    public VirtualJoystick joystick; // Script du joystick

    private Vector2 targetPosition;
    private Vector2 moveInput;

    [Header("État")]
    public bool isDead = false;
    public bool isHit = false;

    [Header("Références")]
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public GameObject inventoryGO;
    private Inventory _inventory;

    private bool isTouching = false;
    private bool isRunning = false;

    public float attractionXpRadius = 15f; // Distance d'attraction
    public float attractionXpSpeed = 8f; // Vitesse d'attraction

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        _inventory = inventoryGO.GetComponent<Inventory>();
        targetPosition = rb.position;
    }

    void Start()
    {
        // Détermine si on utilise le joystick (mobile) ou le tap (montre)
        if (CanvaManager.isWatch.HasValue && CanvaManager.isWatch.Value)
        {
            _useJoystick = false;
            joystickUiGo.SetActive(false);
        }
        else
        {
            _useJoystick = true;
            joystickUiGo.SetActive(true);
        }
    }

    // === MODE TAP / TOUCHSCREEN ===
    public void OnTouch(InputAction.CallbackContext context)
    {
        if (_useJoystick || isDead || isHit) return;

        if (context.performed)
        {
            isTouching = true;
            Vector2 screenPos = Pointer.current.position.ReadValue();
            targetPosition = Camera.main.ScreenToWorldPoint(screenPos);
            StartRunningAnimation();
        }
        else if (context.canceled)
        {
            isTouching = false;
            StopRunningAnimation();
        }
    }

    void FixedUpdate()
    {
        if (isDead || isHit) return;

        if (_useJoystick)
            HandleJoystickMovement();
        else
            HandleTapMovement();

       // AttractXPObjects();
    }

    private void HandleJoystickMovement()
    {
        moveInput = joystick != null ? joystick.Direction : Vector2.zero;
        if (moveInput.magnitude > 0.1f)
        {
            // La vitesse dépend de la distance du joystick
            Vector2 velocity = moveInput.normalized * (moveSpeed + _inventory.speed) * moveInput.magnitude;
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);

            UpdateSpriteDirection(moveInput.x);
            StartRunningAnimation();
        }
        else
        {
            StopRunningAnimation();
        }
    }

    private void HandleTapMovement()
    {
        if (!isTouching)
        {
            StopRunningAnimation();
            return;
        }

        Vector2 direction = targetPosition - rb.position;

        if (direction.magnitude > stopThreshold)
        {
            Vector2 velocity = direction.normalized * (moveSpeed + _inventory.speed);
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);

            UpdateSpriteDirection(direction.x);
            StartRunningAnimation();
        }
        else
        {
            StopRunningAnimation();
        }
    }

    // === Helpers pour animations et flip sprite ===
    private void StartRunningAnimation()
    {
        if (!isRunning)
        {
            animator.Play("PlayerRun");
            isRunning = true;
        }
    }

    private void StopRunningAnimation()
    {
        if (isRunning)
        {
            animator.Play("PlayerIdle");
            isRunning = false;
        }
    }

    private void UpdateSpriteDirection(float horizontal)
    {
        if (horizontal > 0.01f) spriteRenderer.flipX = false;
        else if (horizontal < -0.01f) spriteRenderer.flipX = true;
    }

    // === ATTRACTION DES OBJETS XP ===
    private void AttractXPObjects()
    {
        GameObject[] xpObjects = GameObject.FindGameObjectsWithTag("XP");


        foreach (GameObject xpObject in xpObjects)
        {
            if (xpObject == null) continue;

            float distance = Vector2.Distance(transform.position, xpObject.transform.position);

            // Si l'XP est dans le rayon d'attraction
            if (distance < attractionXpRadius)
            {
                Vector2 direction = (transform.position - xpObject.transform.position).normalized;
                Rigidbody2D xpRb = xpObject.GetComponent<Rigidbody2D>();

                if (xpRb != null)
                {
                    xpRb.linearVelocity = direction * attractionXpSpeed;
                }
            }
        }
    }
    
    // 🧩 Visualisation dans la scène
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attractionXpRadius);
    }

    public void Die()
    {
        if (_inventory.lifeCount > 1)
        {
            _inventory.lifeCount -= 1;
            isHit = true;
            animator.Play("PlayerHit");
            StartCoroutine(ResetHitStateAfterDelay(1f));

            GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");
            foreach (GameObject zombie in zombies)
            {
                Zombie zombieScript = zombie.GetComponent<Zombie>();
                if (zombieScript != null)
                {
                    Vector2 knockbackDir = ((Vector2)zombie.transform.position - (Vector2)transform.position).normalized;
                    zombieScript.ApplyKnockback(knockbackDir, 30f);
                }
            }
        }
        else
        {
            isDead = true;
            animator.Play("PlayerDie");
            StartCoroutine(LoseScreenCoroutine(1.5f));
            Destroy(gameObject, 2f);
        }
    }

    private IEnumerator ResetHitStateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isHit = false;
    }

    private IEnumerator LoseScreenCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameManager gameManager = FindFirstObjectByType<GameManager>();
        if (gameManager != null)
            gameManager.displayLoseScreen();
    }
}
