using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scenes.Inventory.Scripts
{
    public class UIViewModel : MonoBehaviour
    {
        public InventoryConfig config;
        public UIDocument UI;
        private Button bagButton;
        private VisualElement panel;

        private bool isOpen = false;

        void OnEnable()
        {
            
            var root = UI.rootVisualElement;
            bagButton = root.Q<Button>("bag");

            if (bagButton == null) {
                throw new System.Exception("Bag button not found. Please add a button with the 'bag' name.");
            }

            panel = root.Q<VisualElement>("panel");
            panel.RemoveFromClassList("open");

            if (panel == null) {
                throw new System.Exception("Panel not found. Please add a panel with the 'panel' name.");
            }


            bagButton.clicked += ToggleBag;
        }

        void ToggleBag()
        {
            isOpen = !isOpen;

            if (isOpen) {
                panel.AddToClassList("open");
                bagButton.AddToClassList("open");
            } else {
                panel.RemoveFromClassList("open");
                bagButton.RemoveFromClassList("open");
            }
        }

        void Start()
        {
            if (config == null) {
                throw new System.Exception("Inventory config not assigned. Please assign a InventoryConfig object in the inspector.");
            }
        }

    }
}