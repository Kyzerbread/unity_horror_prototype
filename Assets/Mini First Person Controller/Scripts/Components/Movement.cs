using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float sprintMultiplier = 1.5f;
    public float mouseSensitivity = 100f;
    public float jumpForce = 5f;

    private Rigidbody rb;
    private Camera playerCamera;
    private float xRotation = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        playerCamera = GetComponentInChildren<Camera>();
    }

    public void Move(Vector2 input, bool sprinting)
    {
        float speed = sprinting ? walkSpeed * sprintMultiplier : walkSpeed;

        Vector3 moveDir = transform.right * input.x + transform.forward * input.y;
        Vector3 targetVelocity = moveDir * speed;
        Vector3 velocityChange = targetVelocity - rb.linearVelocity;
        velocityChange.y = 0;

        rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }

    public void Look(Vector2 lookDelta)
    {
        float mouseX = lookDelta.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookDelta.y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    public void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
}
