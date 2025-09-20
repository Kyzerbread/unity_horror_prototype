using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Item> items = new List<Item>();

    public void Add(Item item) => items.Add(item);

    public void UseItem(int index, IInteractor interactor)
    {
        if (index >= 0 && index < items.Count)
        {
            bool removeAfterUse = items[index].Use(interactor);
            if (removeAfterUse)
            {
                items.RemoveAt(index);
            }
            
        }
    }
}