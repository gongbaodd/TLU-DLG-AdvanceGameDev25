using UnityEngine;
using UnityEngine.UI;


public class InventoryUI : MonoBehaviour
{
    Inventory inventory; // Reference to the Inventory instance

    public Button bagButton; // Button to open the inventory bag
    public Transform itemsParent; // Parent object for the inventory slots
    public GameObject inventoryUI; // Reference to the inventory UI object

    InventorySlot[] slots; // Array of inventory slots

    void Start()
    {

        inventory = Inventory.instance; // Get the Inventory instance
        inventory.onItemChangedCallback += UpdateUI; // Subscribe to the item changed callback

        slots = itemsParent.GetComponentsInChildren<InventorySlot>(); // Get all inventory slots in the parent object
        inventory.LoadInventory(inventory.data); // Load the inventory data
        UpdateUI(); // Update the UI to reflect the current inventory state

        if (bagButton != null)
        {
            bagButton.onClick.AddListener(() => inventoryUI.SetActive(!inventoryUI.activeSelf)); // Toggle the inventory UI on button click
        }

    }

    void Update()
    {


    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.data.items.Count) // If there is an item in the inventory
            {
                slots[i].AddItem(inventory.data.items[i]); // Add the item to the slot
            }
            else
            {
                slots[i].ClearSlot(); // Clear the slot if no item is present
            }
        }
    }

    void OnDisable()
    {
        if (inventory != null)
        {
            inventory.onItemChangedCallback -= UpdateUI; // Unsubscribe from the item changed callback
        }
    }
}