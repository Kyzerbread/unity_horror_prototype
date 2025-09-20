using UnityEngine;

public class Jump : MonoBehaviour
{
    private InputHandler input;
    Rigidbody rigidbody;
    public float jumpStrength = 2;
    public event System.Action Jumped;

    [SerializeField, Tooltip("Prevents jumping when the transform is in mid-air.")]
    GroundCheck groundCheck;

    void Reset()
    {
        // Try to get groundCheck.
        groundCheck = GetComponentInChildren<GroundCheck>();
    }

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        input = GetComponent<InputHandler>();
    }

    void LateUpdate()
    {
        // Jump when the Jump button is pressed and we are on the ground.
        if (input.JumpRequested && (!groundCheck || groundCheck.isGrounded))
        {
            rigidbody.AddForce(Vector3.up * 100 * jumpStrength);
            Jumped?.Invoke();
            input.ResetJump();
        }
    }
}
