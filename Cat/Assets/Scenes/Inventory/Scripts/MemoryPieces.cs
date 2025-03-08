using System;
using UnityEngine;

namespace Assets.Scenes.Inventory.Scripts
{
    [CreateAssetMenu(fileName = "MemoryPieces", menuName = "Scriptable Objects/MemoryPieces")]
    [Serializable]
    public class MemoryPieces : ScriptableObject
    {
        public bool hasFruitNinjaMemory = false;
        public bool hasDiabloMemory = false;
    }
}


