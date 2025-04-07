using UnityEngine;


public class InventoryUI : MonoBehaviour
{
    Inventory inventory; // Reference to the Inventory instance

    public Transform itemsParent; // Parent object for the inventory slots
    public GameObject inventoryUI; // Reference to the inventory UI object

    InventorySlot[] slots; // Array of inventory slots

    void Start()
    {

        inventory = Inventory.instance; // Get the Inventory instance
        inventory.onItemChangedCallback += UpdateUI; // Subscribe to the item changed callback

        slots = itemsParent.GetComponentsInChildren<InventorySlot>(); // Get all inventory slots in the parent object

        //UpdateUI(); // Initial update of the UI
    }

    void Update()
    {
        // Toggle the inventory UI on/off with the "I" key
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf); // Toggle the active state of the inventory UI
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count) // If there is an item in the inventory
            {
                slots[i].AddItem(inventory.items[i]); // Add the item to the slot
            }
            else
            {
                slots[i].ClearSlot(); // Clear the slot if no item is present
            }
        }
    }
}