using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scenes.Inventory.Scripts
{
    public class UIViewModel : MonoBehaviour
    {
        public InventoryConfig config;
        public UIDocument UI;
        private Button bagButton;

        private bool IsBagOpen { 
            get { 
                return config.inventoryDisplayState != DisplayStyle.None; 
            }

            set {
                if (value) {
                    config.inventoryDisplayState = DisplayStyle.Flex;
                } else {
                    config.inventoryDisplayState = DisplayStyle.None;
                }
            }
        }

        void OnEnable()
        {
            var root = UI.rootVisualElement;
            bagButton = root.Q<Button>("bag");

            bagButton.clicked += ToggleBag;
        }

        void ToggleBag()
        {
            IsBagOpen = !IsBagOpen;
        }

        void Start()
        {
            if (config == null) {
                throw new System.Exception("Inventory config not assigned. Please assign a InventoryConfig object in the inspector.");
            }
        }

    }
}