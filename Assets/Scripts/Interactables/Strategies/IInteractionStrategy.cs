
public interface IInteractionStrategy
{
    InteractionType Type { get; }
    string InteractionText { get; }
    void Interact(IInteractor interactor);
}

public interface IHoverableInteraction
{
    void OnInteractorHoverEnter();
    void OnInteractorHoverExit();
}