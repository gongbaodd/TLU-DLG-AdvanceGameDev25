using TMPro;
using UnityEngine;

namespace Assets.Scenes.Diablo.Scripts
{
    public class CursorController : MonoBehaviour
    {
        [SerializeField] GameObject interactableCanvas;
        CursorLabelController shownCursorLabel;
        CursorLabelController lastInteractable;
        RaycastHit? HitInteractables()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(ray.origin, ray.direction * 200.0f, Color.green, 1.0f);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                return hit;
            }

            return null;
        }

        public Vector3? HitPoint()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                return hit.point;
            }
            return null;
        }

        void ShowLabel(RaycastHit interactable)
        {
            HideLabel();

            var transform = interactable.transform;
            var labelController = transform.gameObject.GetComponent<CursorLabelController>();
            if (labelController)
            {
                labelController.ShowLabel();
                shownCursorLabel = labelController;
            }
        }

        void HideLabel()
        {
            if (shownCursorLabel)
            {
                shownCursorLabel.HideLabel();
            }
        }

        void Interact(RaycastHit interactable) {
            var labelController = interactable.transform.gameObject.GetComponent<CursorLabelController>();

            if (labelController) {
                labelController.Interact();
                lastInteractable = labelController;
            }
        }

        void ClearInteract() {
            if (lastInteractable) {
                lastInteractable.ClearInteract();
                lastInteractable = null;
            }
        }

        void Awake()
        {
            HideLabel();
        }

        void Update()
        {

            if (Input.GetMouseButtonDown(0))
            {
                ClearInteract();
            }


            var interactables = HitInteractables();

            if (interactables != null)
            {
                var interactable = interactables.Value;
                ShowLabel(interactable);
                if (Input.GetMouseButtonDown(0))
                {
                    Interact(interactable);
                }
            }
            else
            {
                HideLabel();
            }
        }
    }
}


