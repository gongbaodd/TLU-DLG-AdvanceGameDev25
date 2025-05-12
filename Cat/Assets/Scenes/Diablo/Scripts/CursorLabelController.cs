using TMPro;
using UnityEngine;

namespace Assets.Scenes.Diablo.Scripts
{
    public class CursorLabelController : MonoBehaviour
    {
        [SerializeField] GameObject InteractableCanvas;

        readonly string BOXTAG = BoxController.BOXTAG;
        readonly string ENEMYTAG = "DiabloEnemy";

        GameObject instance;

        LevelController manager;

        public void ShowLabel()
        {

            var gameConfig = LevelController.config;
            var labelObject = instance.transform.Find("Label");

            if (labelObject == null) return;

            var label = labelObject.GetComponent<TextMeshProUGUI>();

            if (transform.CompareTag(BOXTAG))
            {
                label.text = gameConfig.boxCursorLabel;
            }

            if (transform.CompareTag(ENEMYTAG))
            {
                label.text = gameConfig.enemyCursorLabel;
            }

            instance.SetActive(true);
        }

        public void HideLabel()
        {
            instance.SetActive(false);
        }

        public void Interact()
        {
            if (transform.CompareTag(BOXTAG))
            {
                var stateController = GetComponent<BoxStateController>();
                stateController.Interact();
            }
        }

        public void ClearInteract()
        {
            if (transform.CompareTag(BOXTAG))
            {
                var stateController = GetComponent<BoxStateController>();
                stateController.ClearInteract();
            }
        }

        void Awake()
        {
            instance = Instantiate(InteractableCanvas, transform);
            instance.SetActive(false);
        }

        void Start()
        {
            manager = LevelController.Instance;
        }

        // Update is called once per frame
        void Update()
        {
            if (instance.activeSelf)
            {
                instance.transform.position = transform.position + Vector3.up * 1.5f;
                instance.transform.LookAt(Camera.main.transform.position);
                instance.transform.Rotate(0, 180f, 0);
            }
        }
    }
}

