using UnityEngine;

/* The base item class. All items should derive from this. */

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{

    new public string name = "New Item";    // Name of the item
    public Sprite icon = null;              // Item icon
    public bool isDefaultItem = false;      // Is the item default wear?

    // Called when the item is pressed in the inventory
    public virtual void Use()
    {
        // Use the item
        UIManager.Instance.ShowNotificationPanel("Using memory piece"); // Show the notification panel if it's open
        Debug.Log("Using " + name);
        RemoveFromInventory(); // Remove the item from the inventory
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }

}
