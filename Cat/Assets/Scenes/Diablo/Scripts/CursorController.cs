using UnityEngine;

namespace Assets.Scenes.Diablo.Scripts
{
    public class CursorController : MonoBehaviour
    {
        [SerializeField] GameObject interactableCanvas;
        public RaycastHit? HitInteractables() {
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

        void Awake()
        {
            interactableCanvas.SetActive(false);
        }

        void Update()
        {
            var interactables = HitInteractables();

            if (interactables != null) {
                var transform = interactables.Value.transform;
                if (transform.CompareTag(BoxController.BOXTAG)) {
                    interactableCanvas.transform.position = transform.position + Vector3.up;
                    interactableCanvas.SetActive(true);
                    interactableCanvas.transform.LookAt(Camera.main.transform.position);
                }
            } else {
                interactableCanvas.SetActive(false);
            }
        }
    }
}


