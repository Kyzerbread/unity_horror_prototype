using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField] private Transform character; // the body (with CharacterController)
    [SerializeField] private InputHandler input;

    [Header("Settings")]
    public float sensitivity = 2f;   // mouse/gamepad sensitivity
    public float smoothing = 1.5f;   // higher = smoother, lower = snappier

    private Vector2 velocity;        // accumulated rotation
    private Vector2 frameVelocity;   // smoothed frame rotation

    private void Reset()
    {
        // Automatically grab the parent’s transform if not set
        var movement = GetComponentInParent<FirstPersonMovement>();
        if (movement != null)
            character = movement.transform;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Get look input from InputHandler
        Vector2 lookDelta = input.LookInput;

        // Apply sensitivity
        Vector2 rawFrameVelocity = lookDelta * sensitivity;

        // Smooth the input
        frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1f / smoothing);

        // Accumulate total rotation
        velocity += frameVelocity;
        velocity.y = Mathf.Clamp(velocity.y, -90f, 90f); // clamp pitch

        // Apply pitch to the camera (this script is on the camera)
        transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);

        // Apply yaw to the character body
        if (character != null)
            character.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);
    }
}
