using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float velocidadMovimiento = 5.0f;   // 移动速度
    public float fuerzaSalto = 5.0f;           // 跳跃力度
    public LayerMask groundMask;              // 地面层
    public Transform groundCheck;             // 脚下检测点
    public float groundCheckRadius = 0.2f;     // 检测范围

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 检测是否站在地面上
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);

        // 获取输入
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 inputDirection = new Vector3(horizontal, 0f, vertical).normalized;

        // 计算目标位置
        Vector3 move = inputDirection * velocidadMovimiento * Time.deltaTime;
        rb.MovePosition(rb.position + move);

        // 跳跃
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
        }
    }
}
