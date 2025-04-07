using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scenes.Inventory.Scripts
{
    [CreateAssetMenu(fileName = "InventoryConfig", menuName = "Inventory/config")]
    [Serializable]
    public class InventoryConfig : ScriptableObject
    {
        public bool hasFruitNinjaMemory = false;
        public bool hasDiabloMemory = false;

        public StyleEnum<DisplayStyle> inventoryDisplayState = new(DisplayStyle.None);
    }
}


