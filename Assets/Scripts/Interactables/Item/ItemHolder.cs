using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    [SerializeField] private Item itemData;
    public Item ItemData => itemData;
}