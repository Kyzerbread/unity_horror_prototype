using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] private MonoBehaviour strategyComponent;
    private IInteractionStrategy strategy;

    public IInteractionStrategy Strategy => strategy;

    private void Awake()
    {
        strategy = strategyComponent as IInteractionStrategy;
        if (strategy == null)
        {
            Debug.LogError($"{strategyComponent.name} does not implement IInteractionStrategy!");
        }
    }

    public void Interact(IInteractor interactor) => strategy.Interact(interactor);
    public void OnInteractorHoverEnter(IInteractionStrategy strategy)
    {

    }

    public void OnInteractorHoverExit()
    {
        // TODO: do something?
    }
}
