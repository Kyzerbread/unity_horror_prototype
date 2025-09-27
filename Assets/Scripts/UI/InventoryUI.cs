using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class InventoryUI : MonoBehaviour
{
    private List<Item> items = new List<Item>();
    [SerializeField] private Inventory inventory;

    private UIDocument uiDocument;
    private ListView listView;

    private bool isOpen = false;
    public bool IsOpen => isOpen;
 
    void Awake()
    {
        var uiDoc = GetComponent<UIDocument>();
        var root = uiDoc.rootVisualElement;
        listView = root.Q<ListView>("InventoryList");

        items = inventory.Items;
        SetupListView();
    }

    private void OnEnable()
    {
        inventory.OnInventoryUpdated += HandleInventoryUpdated;
    }

    private void OnDisable()
    {
        inventory.OnInventoryUpdated -= HandleInventoryUpdated;
    }

    public void Toggle()
    {
        isOpen = !isOpen;
        uiDocument.rootVisualElement.style.display =
            isOpen ? DisplayStyle.Flex : DisplayStyle.None;
    }

    private void HandleInventoryUpdated(List<Item> updatedItems)
    {
        Debug.Log("Picked up inventory Event!");
        items = updatedItems;
        listView.itemsSource = items;
        listView.RefreshItems();
    }

    private void SetupListView()
    {
        listView.itemsSource = items;

        // Define how each row looks
        listView.makeItem = () =>
        {
            Label label = new Label();
            return label;
        };

        // Define how to bind data to each row
        listView.bindItem = (element, i) =>
        {
            var label = (Label)element;
            label.text = items[i].name; // assuming Item has ItemName
        };

        listView.selectionType = SelectionType.Single;

        // Handle selection (clicking an item)
        listView.onSelectionChange += (selected) =>
        {
            foreach (var obj in selected)
            {
                Item item = obj as Item;
                if (item != null)
                {
                    Debug.Log($"Selected {item.name}");
                    // Optionally: inventory.Use(item);
                }
            }
        };
    }
}
