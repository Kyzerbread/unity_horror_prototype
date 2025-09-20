using UnityEngine;
[RequireComponent(typeof(InputHandler))]
public class FirstPersonLook : MonoBehaviour
{
    [SerializeField] private Transform character;
    private InputHandler input;
    private Movement movement;

    [Header("Settings")]
    public float smoothing = 1.5f;

    private Vector2 velocity;
    private Vector2 frameVelocity;

    private void Awake()
    {
        input = GetComponent<InputHandler>();
    }

    private void Reset()
    {
        // Automatically find the character (the body) if not set
        var movement = GetComponentInParent<FirstPersonMovement>();
        if (movement != null)
        {
            character = movement.transform;
        }
    }

    private void Start()
    {
        // Lock the mouse cursor to the game screen.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Use the new Input System LookInput (Vector2)
        Vector2 mouseDelta = input.LookInput;

        // Smooth the input
        Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one);
        frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1f / smoothing);

        // Update velocity (cumulative rotation)
        velocity += frameVelocity;
        velocity.y = Mathf.Clamp(velocity.y, -90f, 90f);

        // Rotate camera up-down and character left-right
        transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right); // Pitch
        if (character != null)
        {
            character.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up); // Yaw
        }
    }
}
