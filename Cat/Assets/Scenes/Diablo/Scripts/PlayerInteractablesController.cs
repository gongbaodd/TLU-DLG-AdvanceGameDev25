using UnityEngine;

namespace Assets.Scenes.Diablo.Scripts
{
    public class PlayerInteractablesController : MonoBehaviour
    {
        private GameObject interactable;

        public void SetInteractWith(GameObject item)
        {
            interactable = item;
        }

        public bool CompareInteractable(GameObject item) {
            return item == interactable;
        }

        public void ResetInteractWith() {
            interactable = null;
        }
    }
}