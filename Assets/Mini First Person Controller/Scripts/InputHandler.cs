using UnityEngine;
using System;

public class InputHandler : MonoBehaviour
{
    private PlayerControls controls;

    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool IsSprinting { get; private set; }

    public bool IsCrouching { get; private set; }
    public bool JumpRequested { get; private set; }
    public event Action OnInteract;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Player.Move.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => MoveInput = Vector2.zero;

        controls.Player.Look.performed += ctx => LookInput = ctx.ReadValue<Vector2>();
        controls.Player.Look.canceled += ctx => LookInput = Vector2.zero;

        controls.Player.Sprint.performed += ctx => IsSprinting = ctx.ReadValueAsButton();
        controls.Player.Sprint.canceled += ctx => IsSprinting = false;

        controls.Player.Jump.performed += ctx => JumpRequested = true;

        controls.Player.Interact.performed += ctx => OnInteract?.Invoke();

        controls.Player.Crouch.performed += ctx => IsCrouching = ctx.ReadValueAsButton();
        controls.Player.Crouch.canceled += ctx => IsCrouching = false;
    }

    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();

    public void ResetJump() => JumpRequested = false;
}
