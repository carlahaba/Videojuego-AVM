using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5.0f;        // Movement speed
    [SerializeField] private float jumpForce = 5.0f;        // Jump force
    [SerializeField] private float fallMultiplier = 2.5f;   // Fall gravity multiplier
    [SerializeField] private float lowJumpMultiplier = 2.0f;// Low jump gravity multiplier
    [SerializeField] private float maxFallSpeed = 10f;      // Maximum fall speed

    [Header("Ground Detection")]
    [SerializeField] private LayerMask groundMask;          // Ground layer
    [SerializeField] private Transform groundCheck;         // Ground check point
    [SerializeField] private float groundCheckRadius = 0.2f;// Detection radius
    [SerializeField] private float groundDelay = 0.05f;     // Ground confirmation delay

    // Component references
    private Rigidbody rb;
    private Animator animator;

    // State tracking
    private bool isGrounded;
    private bool isJumping;
    private bool jumpKeyReleased = true;
    private float groundedTime = 0f;

    private void Start()
    {
        InitializeComponents();
    }

    private void Update()
    {
        CheckGrounded();
        UpdateGroundedTime();
        HandleJumpInput();
        ApplyExtraGravity();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    /// Initialize component references
    private void InitializeComponents()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        if (!groundCheck)
        {
            Debug.LogWarning("Ground check transform not assigned!");
            groundCheck = transform;
        }
    }

    /// Update ground contact time
    private void UpdateGroundedTime()
    {
        if (isGrounded)
            groundedTime += Time.deltaTime;
        else
            groundedTime = 0f;
    }


    /// Apply extra gravity for better jump feel
    private void ApplyExtraGravity()
    {
        // If falling, apply fall gravity
        if (rb.velocity.y < 0)
        {
            rb.AddForce(Vector3.down * fallMultiplier * Physics.gravity.magnitude, ForceMode.Acceleration);
            
            // Limit maximum fall speed
            if (rb.velocity.y < -maxFallSpeed)
            {
                rb.velocity = new Vector3(rb.velocity.x, -maxFallSpeed, rb.velocity.z);
            }
        }
        // If rising but jump button not held, apply low jump gravity
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.AddForce(Vector3.down * lowJumpMultiplier * Physics.gravity.magnitude, ForceMode.Acceleration);
        }
    }


    /// Check if character is grounded
    private void CheckGrounded()
    {
        bool wasGrounded = isGrounded;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);
        
        // If rising, force not grounded state
        if (isJumping && rb.velocity.y > 0.1f)
        {
            isGrounded = false;
        }
        
        UpdateAnimator(wasGrounded);
    }

    /// Update animation states
    private void UpdateAnimator(bool wasGrounded)
    {
        if (!animator) return;
        
        animator.SetBool("isGrounded", isGrounded);
        
        // Only reset jump animation when landing from air
        if (!wasGrounded && isGrounded)
        {
            animator.SetBool("isJumping", false);
        }
    }

    /// Handle movement input
    private void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 inputDirection = new Vector3(horizontal, 0f, vertical).normalized;

        // Only move when there is input
        if (inputDirection.magnitude > 0.1f)
        {
            rb.MovePosition(rb.position + inputDirection * moveSpeed * Time.fixedDeltaTime);
            if (animator) animator.SetBool("isWalking", true);
        }
        else if (animator)
        {
            animator.SetBool("isWalking", false);
        }
    }


    /// Handle jump input
    private void HandleJumpInput()
    {
        // Jump key release detection
        if (Input.GetButtonUp("Jump"))
        {
            jumpKeyReleased = true;
        }
        
        // Handle jump logic
        HandleJumpLogic();
        
        // Handle landing logic
        HandleLandingLogic();
        
        // Debug information
        Debug.Log($"isGrounded: {isGrounded}, isJumping: {isJumping}, groundedTime: {groundedTime}, velocity.y: {rb.velocity.y}");
    }

    /// Handle jump logic
    private void HandleJumpLogic()
    {
        // Only allow jump when grounded, not already jumping, and jump key released then pressed
        bool canJump = isGrounded && groundedTime > groundDelay && !isJumping && jumpKeyReleased;
        
        if (canJump && Input.GetButtonDown("Jump"))
        {
            // Set jump state
            isJumping = true;
            isGrounded = false;
            groundedTime = 0f;
            jumpKeyReleased = false;
            
            // Apply jump force
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            
            if (animator) animator.SetBool("isJumping", true);
        }
    }

    /// Handle landing logic
    private void HandleLandingLogic()
    {
        // Only consider landed when in jump state, grounded for some time and vertical velocity near zero
        if (isJumping && isGrounded && groundedTime > groundDelay && rb.velocity.y < 0.1f)
        {
            isJumping = false;
        }
    }

} 