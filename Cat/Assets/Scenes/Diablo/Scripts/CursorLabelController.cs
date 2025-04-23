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

        public void ShowLabel()
        {

            var gameConfig = DiabloController.config;
            var label = instance.transform.Find("Label")?.GetComponent<TextMeshProUGUI>();

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

        void Awake()
        {
            instance = Instantiate(InteractableCanvas, transform);
            instance.SetActive(false);
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

