using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    private List<Item> items = new List<Item>();
    public List<Item> Items => items;

    public event Action<List<Item>> OnInventoryUpdated;

    public void Add(Item item)
    {
        items.Add(item);
        OnInventoryUpdated?.Invoke(items);
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        OnInventoryUpdated?.Invoke(items);
    }

    public void UseItem(int index, IInteractor interactor)
    {
        if (index >= 0 && index < items.Count)
        {
            bool removeAfterUse = items[index].OnUse(interactor);
            if (removeAfterUse)
            {
                items.RemoveAt(index);
            }
        }
        OnInventoryUpdated?.Invoke(items);
    }
}