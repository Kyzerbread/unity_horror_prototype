using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonMovement : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 4f;
    public float runSpeed = 6f;

    [Header("Ground Control")]
    public float groundAcceleration = 60f;   // fast acceleration
    public float groundDeceleration = 80f;   // very fast stop

    [Header("Air Control")]
    [Range(0f, 1f)]
    public float airControlMultiplier = 0.4f;

    [Header("Jump & Gravity")]
    public float jumpHeight = 2f;
    public float gravity = -9.81f;
    public float massMultiplier = 1.2f;

    private CharacterController controller;
    private InputHandler input;

    private Vector3 velocity;       // total velocity
    private float verticalVel;      // vertical only

    public Vector3 Velocity => velocity;
    public CharacterController Controller => controller;
    public bool IsSprinting => input?.IsSprinting ?? false;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        input = GetComponent<InputHandler>();
    }

    private void OnEnable() => input.JumpPressed += HandleJump;
    private void OnDisable() => input.JumpPressed -= HandleJump;

    private void Update()
    {
        bool isGrounded = controller.isGrounded;

        Vector2 moveInput = input.MoveInput;
        Vector3 desiredDir = (transform.right * moveInput.x + transform.forward * moveInput.y).normalized;
        float targetSpeed = input.IsSprinting ? runSpeed : walkSpeed;

        if (isGrounded)
        {
            Vector3 horizontal = new Vector3(velocity.x, 0f, velocity.z);
            Vector3 desiredVel = desiredDir * targetSpeed;

            if (desiredDir.magnitude > 0f)
            {
                // Very snappy acceleration
                horizontal = Vector3.MoveTowards(
                    horizontal,
                    desiredVel,
                    groundAcceleration * Time.deltaTime
                );
            }
            else
            {
                // Very snappy deceleration
                horizontal = Vector3.MoveTowards(
                    horizontal,
                    Vector3.zero,
                    groundDeceleration * Time.deltaTime
                );
            }

            velocity = new Vector3(horizontal.x, verticalVel, horizontal.z);

            if (verticalVel < 0)
                verticalVel = -2f; // stick to ground
        }
        else
        {
            // Air inertia with small steering influence
            Vector3 horizontal = new Vector3(velocity.x, 0f, velocity.z);
            Vector3 airDesired = desiredDir * targetSpeed;

            horizontal = Vector3.Lerp(horizontal, airDesired, airControlMultiplier * Time.deltaTime);

            velocity = new Vector3(horizontal.x, verticalVel, horizontal.z);
        }

        // Apply gravity
        verticalVel += gravity * massMultiplier * Time.deltaTime;
        velocity.y = verticalVel;

        controller.Move(velocity * Time.deltaTime);
    }

    private void HandleJump()
    {
        if (controller.isGrounded)
            verticalVel = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
}
