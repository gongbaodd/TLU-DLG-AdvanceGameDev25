using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class InventoryLoader : MonoBehaviour
{
    public string inventoryAddress = "Assets/Items/PlayerInventoryData.asset"; // Address of the InventoryData asset

    void Start()
    {
        Addressables.LoadAssetAsync<InventoryData>(inventoryAddress).Completed += OnLoaded;
    }

    void OnLoaded(AsyncOperationHandle<InventoryData> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Inventory.instance.data = handle.Result;
            Debug.Log("Inventory data successfully loaded.");

            if (Inventory.instance.onItemChangedCallback != null)
            {
                Inventory.instance.onItemChangedCallback.Invoke(); // Trigger the item changed callback to update the UI
            }
        }
        else
        {
            Debug.LogError("Failed to load inventory data from address: " + inventoryAddress);
        }
    }
}