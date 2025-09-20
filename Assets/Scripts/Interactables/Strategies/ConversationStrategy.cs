using UnityEngine;

public class ConversationStrategy : MonoBehaviour, IInteractionStrategy
{
    public InteractionType Type => InteractionType.Talk;

    public string InteractionText => InteractionTexts.Talk;

    public void Interact(IInteractor interactor)
    {
        // TODO: begin a conversation
    }
}
