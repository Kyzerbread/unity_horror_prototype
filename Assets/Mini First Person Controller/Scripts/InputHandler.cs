using UnityEngine;
using UnityEngine.InputSystem;
using System;

[RequireComponent(typeof(PlayerInput))]
public class InputHandler : MonoBehaviour
{
    // Continuous state (poll these each frame)
    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool IsSprinting { get; private set; }
    public bool IsCrouching { get; private set; }

    // Discrete events
    public event Action JumpPressed;
    public event Action InteractPressed;

    // --- PlayerInput (Send Messages) will call these by *action name* ---

    // Value (Vector2)
    public void OnMove(InputValue value)
    {
        // Will be (0,0) when released; Send Messages fires whenever value changes.
        MoveInput = value.Get<Vector2>();
    }

    // Value (Vector2) — mouse delta / right stick
    public void OnLook(InputValue value)
    {
        LookInput = value.Get<Vector2>();
    }

    // Button (bool/float) — pressed while held
    public void OnSprint(InputValue value)
    {
        Debug.Log("OnSprint" + value.isPressed.ToString());
        // For Button actions, isPressed is true when above press threshold.
        IsSprinting = value.isPressed;
    }

    // Button (bool/float) — pressed while held
    public void OnCrouch(InputValue value)
    {
        IsCrouching = value.isPressed;
    }

    // Button — fire once on press (ignore release)
    public void OnJump(InputValue value)
    {
        if (value.isPressed) JumpPressed?.Invoke();
    }

    // Button — fire once on press (ignore release)
    public void OnInteract(InputValue value)
    {
        if (value.isPressed) InteractPressed?.Invoke();
    }
}
