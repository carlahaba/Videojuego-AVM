using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float velocidadMovimiento = 5.0f;   // Movement speed
    public float fuerzaSalto = 5.0f;           // Jump force

    [Header("Ground Detection")]
    public LayerMask groundMask;              // Ground layer
    public Transform groundCheck;             // Ground check point
    public float groundCheckRadius = 0.2f;     // Detection radius

    private Rigidbody rb;
    private Animator animator;
    private bool isGrounded;
    private int jumpCount = 0;  // Used to track jump count
    private bool jumpPressed = false; // Track if jump button is pressed
    private float jumpCooldown = 0.2f; // Jump cooldown time
    private float lastJumpTime = -1f; // Last jump time

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Ground detection
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);
        animator.SetBool("isGrounded", isGrounded);

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 inputDirection = new Vector3(horizontal, 0f, vertical).normalized;

        animator.SetBool("isWalking", inputDirection.magnitude > 0.1f);
        rb.MovePosition(rb.position + inputDirection * velocidadMovimiento * Time.deltaTime);

        // Reset jump count when grounded
        if (isGrounded)
        {
            jumpCount = 0;
            animator.SetBool("isJumping", false);
        }

        // Jump handling logic optimization
        bool jumpButtonDown = Input.GetButtonDown("Jump");
        
        // Make sure button must be released and pressed again to trigger new jump
        if (!jumpPressed && jumpButtonDown && jumpCount < 2 && Time.time > lastJumpTime + jumpCooldown)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // Reset vertical velocity for consistent jumps
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
            animator.SetBool("isJumping", true);
            jumpCount++;
            lastJumpTime = Time.time;
        }
        
        // Update jump button state
        jumpPressed = Input.GetButton("Jump");
    }
}