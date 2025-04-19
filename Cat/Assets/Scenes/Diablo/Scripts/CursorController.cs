using UnityEngine;

namespace Assets.Scenes.Diablo.Scripts
{
    public class CursorController : MonoBehaviour
    {
        [SerializeField] GameObject interactableCanvas;
        RaycastHit? HitInteractables() {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Debug.DrawRay(ray.origin, ray.direction * 200.0f, Color.green, 1.0f);
            if (Physics.Raycast(ray, out RaycastHit hit)) {
                return hit;
            }

            return null;
        }

        public Vector3? HitPoint() {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                return hit.point;
            }
            return null;
        }

        void ShowLabel(RaycastHit interactable) {
                var transform = interactable.transform;
                if (transform.CompareTag(BoxController.BOXTAG)) {
                    interactableCanvas.transform.position = transform.position + Vector3.up;
                    interactableCanvas.SetActive(true);
                    interactableCanvas.transform.LookAt(Camera.main.transform.position);
                    interactableCanvas.transform.Rotate(0, 180f, 0);
                }
        }

        void HideLabel() {
            interactableCanvas.SetActive(false);
        }

        void Awake()
        {
            HideLabel();
        }

        void Update()
        {
            var interactables = HitInteractables();

            if (interactables != null) {
                var interactable = interactables.Value;
                ShowLabel(interactable);
                if (Input.GetMouseButtonDown(0)) {
                    DiabloController.player.GetComponent<PlayerInteractablesController>()
                        .SetInteractWith(interactable.transform.gameObject);
                }

            } else {
                HideLabel();
            }
        }
    }
}


