using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 7f;
    public GameObject joystickUiGo;
    public VirtualJoystick joystick;
    private Vector2 moveInput;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isRunning = false;

    private float lastMoveX = 0f;
    private float lastMoveY = 0f;
    private string currentAnim;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        joystickUiGo.SetActive(true);
    }

    void FixedUpdate()
    {
        HandleJoystickMovement();
    }

    private void HandleJoystickMovement()
    {
        moveInput = joystick != null ? joystick.Direction : Vector2.zero;

        if (moveInput.magnitude > 0.1f)
        {

            Vector2 velocity = moveInput.normalized * moveSpeed * moveInput.magnitude;

            rb.linearVelocity = velocity;
            lastMoveX = moveInput.x;
            lastMoveY = moveInput.y;
            StartRunningAnimation();
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            StopRunningAnimation();
        }
    }

    // === Helpers pour animations et flip sprite ===
    private void StartRunningAnimation()
    {
        string newAnim = currentAnim;

        if (moveInput.x > 0.01f)
            newAnim = "PlayerRunRight";
        else if (moveInput.x < -0.01f)
            newAnim = "PlayerRunLeft";
        else if (moveInput.y > 0.01f)
            newAnim = "PlayerRunUp";
        else if (moveInput.y < -0.01f)
            newAnim = "PlayerRun";

        if (newAnim != currentAnim)
        {
            animator.Play(newAnim);
            currentAnim = newAnim;
        }

        isRunning = true;
    }

    private void StopRunningAnimation()
    {
        if (!isRunning) return;

        string idleAnim = "PlayerIdle";

        if (lastMoveX > 0.01f) idleAnim = "PlayerIdleRight";
        else if (lastMoveX < -0.01f) idleAnim = "PlayerIdleLeft";
        else if (lastMoveY > 0.01f) idleAnim = "PlayerIdleUp";

        animator.Play(idleAnim);
        currentAnim = idleAnim;
        isRunning = false;
    }
}
