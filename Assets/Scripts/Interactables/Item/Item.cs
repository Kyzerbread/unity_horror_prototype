using UnityEngine;

public abstract class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon;

    public abstract bool Use(IInteractor interactor);
}
