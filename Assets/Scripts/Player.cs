using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    [Header("Ground Detection")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.2f;

    private Rigidbody2D rb;
    private float moveInput;
    private bool isGrounded;
    private bool jumpPressed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Check if player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Get movement input only if grounded
        if (isGrounded)
        {
            moveInput = Input.GetAxisRaw("Horizontal");
        }
        else
        {
            moveInput = 0f; // Prevent movement in air
        }

        // Jump input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpPressed = true;
        }
    }

    private void FixedUpdate()
    {
        // Move the player only when grounded
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Handle jumping
        if (jumpPressed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpPressed = false;
        }
    }
}
