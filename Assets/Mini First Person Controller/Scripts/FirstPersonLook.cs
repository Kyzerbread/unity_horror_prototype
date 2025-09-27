using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField] private Transform character; // player body
    [SerializeField] private InputHandler input;

    [Header("Settings")]
    public float sensitivity = 2f;          // mouse/gamepad sensitivity
    public float rotationSmoothTime = 0.03f; // 20–30ms is typical for responsive smoothing

    private Vector2 targetRotation;   // raw input-driven rotation
    private Vector2 smoothedRotation; // what we actually apply to camera
    private Vector2 rotationVelocity; // ref for SmoothDamp

    private void Reset()
    {
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
        // --- INPUT ---
        Vector2 lookDelta = input.LookInput * sensitivity;

        // Update the target rotation instantly (raw input)
        targetRotation.x += lookDelta.x;
        targetRotation.y -= lookDelta.y;
        targetRotation.y = Mathf.Clamp(targetRotation.y, -90f, 90f);

        // --- SMOOTHING ---
        smoothedRotation.x = Mathf.SmoothDamp(
            smoothedRotation.x, targetRotation.x,
            ref rotationVelocity.x, rotationSmoothTime);

        smoothedRotation.y = Mathf.SmoothDamp(
            smoothedRotation.y, targetRotation.y,
            ref rotationVelocity.y, rotationSmoothTime);

        // --- APPLY ---
        // Pitch (camera only)
        transform.localRotation = Quaternion.Euler(smoothedRotation.y, 0f, 0f);

        // Yaw (character body)
        if (character != null)
            character.localRotation = Quaternion.Euler(0f, smoothedRotation.x, 0f);
    }
}
