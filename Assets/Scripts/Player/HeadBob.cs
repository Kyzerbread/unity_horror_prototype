using UnityEngine;

public class HeadBob : MonoBehaviour
{
    [Header("Headbob Settings")]
    public float baseBobFrequency = 1.5f;   // how fast steps happen
    public float baseBobAmplitude = 0.05f;  // how high the bob is
    public float runAmplitudeMultiplier = 1.5f;
    public float smoothing = 6f;

    [Header("References")]
    public FirstPersonMovement movement; // your movement script

    private float bobTimer;
    private Vector3 originalLocalPos;

    void Start()
    {
        originalLocalPos = transform.localPosition;
    }
    void Update()
    {
        Vector3 velocity = movement.Velocity;
        Vector3 horizontalVel = new Vector3(velocity.x, 0f, velocity.z);
        float speed = horizontalVel.magnitude;

        if (speed > 0.1f && movement.Controller.isGrounded)
        {
            // frequency tied to speed
            float frequency = baseBobFrequency * (speed / movement.walkSpeed);

            // bigger bob when sprinting
            float amplitude = baseBobAmplitude;
            if (movement.IsSprinting)
                amplitude *= runAmplitudeMultiplier;

            bobTimer += Time.deltaTime * frequency;

            // vertical bob (sin wave)
            float verticalBob = Mathf.Sin(bobTimer * 2f) * amplitude;

            // subtle horizontal sway (optional)
            float horizontalBob = Mathf.Cos(bobTimer) * amplitude * 0.25f;

            transform.localPosition = new Vector3(
                originalLocalPos.x + horizontalBob,
                originalLocalPos.y + verticalBob,
                originalLocalPos.z
            );
        }
        else
        {
            // reset when idle
            bobTimer = 0f;
            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                originalLocalPos,
                Time.deltaTime * smoothing
            );
        }
    }
}
