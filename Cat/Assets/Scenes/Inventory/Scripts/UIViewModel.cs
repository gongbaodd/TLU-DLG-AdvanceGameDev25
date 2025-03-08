using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scenes.Inventory.Scripts
{
    public class UIViewModel : MonoBehaviour
    {
        public MemoryPieces memoryPieces;
        public UIDocument UI;

        private VisualElement panel;
        private Button bagButton;

        private bool isBagOpen = false;

        void OnEnable()
        {
            var root = UI.rootVisualElement;
            panel = root.Q<VisualElement>("panel");
            bagButton = root.Q<Button>("bag");

            bagButton.clicked += ToggleBag;
        }

        void ToggleBag()
        {
            isBagOpen = !isBagOpen;
            panel.style.display = isBagOpen ? DisplayStyle.Flex : DisplayStyle.None;
        }

    }
}