using UnityEngine;
using UnityEngine.UI;


public class InventorySlot : MonoBehaviour
{
    public Image icon; // UI element to display the item icon
    public Button removeButton; // Button to remove the item from the inventory

    Item item; // The item assigned to this slot

    public void AddItem(Item newItem)
    {
        item = newItem; // Assign the new item to this slot
        icon.sprite = item.icon; // Set the icon sprite to the item's icon
        icon.enabled = true; // Enable the icon image
        removeButton.interactable = true; // Enable the remove button
    }

    public void ClearSlot()
    {
        item = null; // Clear the item reference
        icon.sprite = null; // Remove the icon sprite
        icon.enabled = false; // Disable the icon image
        removeButton.interactable = false; // Disable the remove button
    }

    public void OnRemoveButton()
    {
        if (item != null) // If there is an item in this slot
        {
            Inventory.instance.Remove(item); // Remove the item from the inventory
        }
    }

    public void UseItem()
    {
        if (item != null) // If there is an item in this slot
        {
            item.Use(); // Use the item
        }
    }

}