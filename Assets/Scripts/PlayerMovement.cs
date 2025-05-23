using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float velocidadMovimiento = 5.0f;   // 移动速度
    public float fuerzaSalto = 5.0f;           // 跳跃力度

    [Header("Ground Detection")]
    public LayerMask groundMask;              // 地面层
    public Transform groundCheck;             // 脚下检测点
    public float groundCheckRadius = 0.2f;     // 检测范围

    private Rigidbody rb;
    private Animator animator;
    private bool isGrounded;
    private int jumpCount = 0;  // 用于记录跳跃次数
    private bool jumpPressed = false; // 跟踪跳跃键是否已被按下
    private float jumpCooldown = 0.2f; // 跳跃冷却时间
    private float lastJumpTime = -1f; // 上次跳跃时间

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 检测地面
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);
        animator.SetBool("isGrounded", isGrounded);

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 inputDirection = new Vector3(horizontal, 0f, vertical).normalized;

        animator.SetBool("isWalking", inputDirection.magnitude > 0.1f);
        rb.MovePosition(rb.position + inputDirection * velocidadMovimiento * Time.deltaTime);

        // 如果在地面上，重置跳跃次数
        if (isGrounded)
        {
            jumpCount = 0;
            animator.SetBool("isJumping", false);
        }

        // 跳跃处理逻辑优化
        bool jumpButtonDown = Input.GetButtonDown("Jump");
        
        // 确保按钮必须先释放再按下才能触发新跳跃
        if (!jumpPressed && jumpButtonDown && jumpCount < 2 && Time.time > lastJumpTime + jumpCooldown)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // 重置竖直速度，确保跳跃一致
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
            animator.SetBool("isJumping", true);
            jumpCount++;
            lastJumpTime = Time.time;
        }
        
        // 更新跳跃按键状态
        jumpPressed = Input.GetButton("Jump");
    }
}