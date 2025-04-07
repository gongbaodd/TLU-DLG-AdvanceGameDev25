using UnityEngine;

public class ItemPickup : Interactable
{

    public Item item;   // Item to put in the inventory on pickup

    // When the player interacts with the item
    public override void Interact()
    {
        base.Interact();

        PickUp();   // Pick it up!
    }

    // Pick up the item
    void PickUp()
    {
        if (item == null)
        {
            Debug.LogWarning("ItemPickup: No item assigned to this pickup.");
            return;
        }

        Debug.Log("Picking up " + item.name);

        if (Inventory.instance == null)
        {
            Debug.LogError("ItemPickup: Inventory instance is not set.");
            return;
        }
        bool wasPickedUp = Inventory.instance.Add(item);    // Add to inventory

        // If successfully picked up
        if (wasPickedUp)
        {
            Destroy(gameObject);    // Destroy item from scene
        }
        else
        {
            Debug.Log("ItemPickup: Inventory is full or item could not be added.");
        }
    }

}
