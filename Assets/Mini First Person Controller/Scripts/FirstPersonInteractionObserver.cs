using System;
using UnityEngine;

public class FirstPersonInteractionObserver : MonoBehaviour, IInteractor
{
    public float playerInteractionRange = 3.0f;
    [SerializeField] private LayerMask interactableMask; // set in inspector

    private Camera playerCamera;
    private IInteractable currentInteractable;
    private InputHandler input;
    private IInteractor interactor;

    public GameObject GameObject => gameObject; // pass through IInteractor interface

    public event Action<IInteractionStrategy> OnHoverEnter;
    public event Action OnHoverExit;

    void Awake()
    {
        input = GetComponent<InputHandler>();
        interactor = GetComponent<IInteractor>();
        playerCamera = GetComponentInChildren<Camera>();
    }

    private void OnEnable()
    {
        input.OnInteract += HandleInteract;
    }

    private void OnDisable()
    {
        input.OnInteract -= HandleInteract;
    }

    private void HandleInteract()
    {
        if (currentInteractable != null)
        {
            currentInteractable.Interact(interactor);
        }
    }

    void Update()
    {
        PollForInteractable();
    }

    private void PollForInteractable()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, playerInteractionRange, interactableMask))
        {
            if (hit.collider.TryGetComponent<IInteractable>(out var interactable))
            {
                HandleHoverEnter(interactable);
            }
            else
            {
                HandleHoverExit();
            }
        }
        else
        {
            HandleHoverExit();
        }
    }
    private void HandleHoverEnter(IInteractable interactable)
    {
        if (currentInteractable == interactable) return;
        currentInteractable = interactable;
        interactable?.OnInteractorHoverEnter(interactable.Strategy);
        OnHoverEnter?.Invoke(interactable.Strategy);
    }

    private void HandleHoverExit()
    {
        if (currentInteractable == null) return;
        currentInteractable?.OnInteractorHoverExit();
        OnHoverExit?.Invoke();
        currentInteractable = null;
    }
}
