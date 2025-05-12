using UnityEngine;

namespace Assets.Scenes.FruitNinja.Scripts
{
    [RequireComponent(typeof(BoxCollider))]
    public class ChestController : MonoBehaviour
    {
        LevelManagerController manager;
        Vector3 targetPos;
        bool isMoving = false;
        [SerializeField] GameObject winVFX;
        [SerializeField] GameObject ChestObj;

        void OnTimerEnd() {
            isMoving = true;
            ChestObj.SetActive(true);
        }

        void MoveToCenter() {
            transform.position = Vector3.Lerp(targetPos, transform.position, 0.05f);
        }

        void Awake()
        {
            winVFX.SetActive(false);
            ChestObj.SetActive(false);
        }

        void Start()
        {
            manager = LevelManagerController.Instance;

            Vector3 screenCenter = new(Screen.width / 2f, Screen.height / 2f, Camera.main.nearClipPlane);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenCenter);
            targetPos = new Vector3(worldPos.x, worldPos.y, transform.position.z);

            TimerController.OnTimerEnd += OnTimerEnd;
        }

        void OnDestroy()
        {
            TimerController.OnTimerEnd -= OnTimerEnd;            
        }

        void Update()
        {
            if (isMoving) {
                MoveToCenter();
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player")) {
                var cursorController = manager.GetComponent<CursorController>();
                if (cursorController.IsDrawing) {
                    var controller = manager.GetComponent<LevelManagerController>();
                    winVFX.SetActive(true);
                    Destroy(gameObject, controller.vfxTime);
                    controller.Win();
                }
            }
        }

    }
}
