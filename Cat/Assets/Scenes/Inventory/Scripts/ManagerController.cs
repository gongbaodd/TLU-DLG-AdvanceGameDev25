using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scenes.Inventory.Scripts
{
    public class ManagerController : MonoBehaviour
    {
        #region Singleton
        public static ManagerController Instance { get; private set; }
        private void Awake()
        {
            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            } else {
                Destroy(gameObject);
            }
        }
        #endregion

        public MemoryPieces memoryPieces;

        public void GotFruitNinjaMemory()
        {
            memoryPieces.hasFruitNinjaMemory = true;
        }

        public void GotDiabloMemory()
        {
            memoryPieces.hasDiabloMemory = true;
        }
    }

}
