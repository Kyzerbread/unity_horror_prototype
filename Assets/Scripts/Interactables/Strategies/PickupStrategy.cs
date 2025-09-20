using UnityEngine;

[RequireComponent(typeof(ItemHolder))]
public class PickupStrategy : MonoBehaviour, IInteractionStrategy
{
    private ItemHolder itemHolder;

    public InteractionType Type => InteractionType.Grab;

    public string InteractionText => InteractionTexts.PickUp;

    private void Awake()
    {
        itemHolder = GetComponent<ItemHolder>();
    }

    public void Interact(IInteractor interactor)
    {
        if (interactor.GameObject.TryGetComponent<Inventory>(out var inventory))
        {
            inventory.Add(itemHolder.ItemData);
            Debug.Log($"{interactor.GameObject.name} picked up {itemHolder.ItemData.name}");
            Destroy(gameObject); // remove pickup from world
        }
    }
}
