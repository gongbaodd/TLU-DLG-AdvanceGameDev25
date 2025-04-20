using TMPro;
using UnityEngine;

namespace Assets.Scenes.Diablo.Scripts
{
    public class CursorController : MonoBehaviour
    {
        readonly string BOXTAG = BoxController.BOXTAG;
        readonly string ENEMYTAG = "DiabloEnemy";

        [SerializeField] GameObject interactableCanvas;
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
            var transform = interactable.transform;
            if (transform.CompareTag(BOXTAG) || transform.CompareTag(ENEMYTAG))
            {
                interactableCanvas.transform.position = transform.position + Vector3.up * 1.5f;
                interactableCanvas.SetActive(true);
                interactableCanvas.transform.LookAt(Camera.main.transform.position);
                interactableCanvas.transform.Rotate(0, 180f, 0);

                var gameConfig = DiabloController.config;
                var label = interactableCanvas.transform.Find("Label")?.GetComponent<TextMeshProUGUI>();

                if (label != null)
                {
                    if (transform.CompareTag(BOXTAG))
                    {
                        label.text = gameConfig.boxCursorLabel;
                    }

                    if (transform.CompareTag(ENEMYTAG))
                    {
                        label.text = gameConfig.enemyCursorLabel;
                    }
                }
            }
        }

        void HideLabel()
        {
            interactableCanvas.SetActive(false);
        }

        void Awake()
        {
            HideLabel();
        }

        void Update()
        {
            var interactables = HitInteractables();

            if (interactables != null)
            {
                var interactable = interactables.Value;
                ShowLabel(interactable);
                if (Input.GetMouseButtonDown(0))
                {
                    DiabloController.player.GetComponent<PlayerInteractablesController>()
                        .SetInteractWith(interactable.transform.gameObject);
                }

            }
            else
            {
                HideLabel();
            }
        }
    }
}


