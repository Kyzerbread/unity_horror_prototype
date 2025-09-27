using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonMovement : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float acceleration = 20f;     // how quickly to build speed
    public float deceleration = 15f;     // how quickly to stop without input

    [Header("Air Control")]
    [Range(0f, 1f)]
    public float airControlMultiplier = 0.2f;

    [Header("Jump & Gravity")]
    public float jumpHeight = 2f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private InputHandler input;

    private Vector3 velocity;        // total velocity (world space)
    private Vector3 horizontalVel;   // horizontal part only
    private float verticalVel;       // vertical part only

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        input = GetComponent<InputHandler>();
    }

    private void OnEnable()
    {
        input.JumpPressed += HandleJump;
    }

    private void OnDisable()
    {
        input.JumpPressed -= HandleJump;
    }

    private void Update()
    {
        bool isGrounded = controller.isGrounded;

        // --- Horizontal movement ---
        Vector2 moveInput = input.MoveInput;
        Vector3 desiredDir = (transform.right * moveInput.x + transform.forward * moveInput.y).normalized;

        // Pick target speed
        float targetSpeed = input.IsSprinting ? runSpeed : walkSpeed;

        // Scale control if in air
        float control = isGrounded ? 1f : airControlMultiplier;

        // Apply acceleration toward desired velocity
        Vector3 desiredVel = desiredDir * targetSpeed;
        horizontalVel = Vector3.MoveTowards(horizontalVel, desiredVel, acceleration * control * Time.deltaTime);

        // Apply deceleration when no input
        if (desiredDir == Vector3.zero)
            horizontalVel = Vector3.MoveTowards(horizontalVel, Vector3.zero, deceleration * control * Time.deltaTime);

        // --- Vertical movement (gravity & jumping) ---
        if (isGrounded && verticalVel < 0)
            verticalVel = -2f; // keep grounded

        verticalVel += gravity * Time.deltaTime;

        // --- Combine ---
        velocity = horizontalVel + Vector3.up * verticalVel;

        // Move controller
        controller.Move(velocity * Time.deltaTime);
    }

    private void HandleJump()
    {
        if (controller.isGrounded)
            verticalVel = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
}
