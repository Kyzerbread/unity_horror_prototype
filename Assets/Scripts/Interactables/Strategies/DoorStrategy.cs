using UnityEngine;

public class DoorStrategy : MonoBehaviour, IInteractionStrategy
{
    private bool isLocked { get; set; }
    public bool IsLocked => isLocked;
    public InteractionType Type => InteractionType.Open;

    public string InteractionText => InteractionTexts.Open;

    public void Interact(IInteractor interactor)
    {
        if (interactor.GameObject)
        {
            UseDoor(interactor.GameObject);
        }
    }

    public void UseDoor(GameObject playerGameObject)
    {
        if (isLocked)
        {
            // handle letting user know the door is locked
        } else
        {
            // move player into the new room
            // trigger the 
        }
    }
}
