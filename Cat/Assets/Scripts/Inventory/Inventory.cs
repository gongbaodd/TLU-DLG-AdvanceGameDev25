using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Reference to the InventoryData ScriptableObject

    #region Singleton

    public static Inventory instance;
    public InventoryData data;
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // Uncomment if you want to keep the inventory across scenes
    }

    public void LoadInventory(InventoryData inventoryData)
    {
        data = inventoryData;
        // Trigger callback to update UI if needed
        onItemChangedCallback?.Invoke();
    }


    #endregion

    // Callback which is triggered when
    // an item gets added/removed.
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 19;	// Amount of slots in inventory
    //public List<Item> items = new List<Item>();

    public bool Add(Item item)
    {
        if (!item.isDefaultItem)
        {
            if (data.items.Count >= space)
            {
                Debug.Log("Inventory full.");
                return false;
            }

            data.items.Add(item);
            //items.Add(item);

            // Trigger callback
            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }

        return true;
    }

    public void Remove(Item item)
    {
        data.items.Remove(item);
        //items.Remove(item);

        // Trigger callback
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public List<Item> GetItems()
    {
        return data.items;
    }
}