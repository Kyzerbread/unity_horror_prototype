using UnityEngine;
using static UnityEditor.Progress;

public enum InteractionType
{
    None,
    Grab,
    Open,
    Talk,
    Use
}
public static class InteractionTexts
{
    public const string PickUp = "Pick Up";
    public const string Open = "Open";
    public const string Talk = "Talk";
    public const string Use = "Use";
    public const string Inspect = "Inspect";
}
public interface IInteractable
{
    IInteractionStrategy Strategy { get; }
    void Interact(IInteractor interactor);
    void OnInteractorHoverEnter(IInteractionStrategy strategy);
    void OnInteractorHoverExit();
}


public interface IInteractor
{
    GameObject GameObject { get; }
}