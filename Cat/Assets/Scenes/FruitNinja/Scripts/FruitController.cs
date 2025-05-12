using System.Collections;
using UnityEngine;

namespace Assets.Scenes.FruitNinja.Scripts
{

    [RequireComponent(typeof(Rigidbody))]
    public class FruitController : MonoBehaviour
    {
        private Rigidbody rb;
        LevelManagerController gameManager;

        [SerializeField] private Spawnables config;

        public event System.Action<GameObject> OnFruitDestroyed;

        public void Spawn()
        {
            var gameManager = LevelManagerController.Instance;
            var spawnContoller = gameManager.GetComponent<SpawnFruitController>();
            var speed = config.speed;
            var torque = config.torque;

            rb = GetComponent<Rigidbody>();
            rb.AddForce(spawnContoller.CalculateForceDirection(transform.position) * speed, ForceMode.Impulse);
            rb.AddTorque(RandomTorque() * torque, ForceMode.Impulse);

            var audioController = gameManager.GetComponent<AudioController>();
            audioController.PlaySpawnSound();
        }

        private Vector3 RandomTorque()
        {
            return new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1));
        }

        public event System.Action<SpawnFruitController.BorderType> OnOutBorder;

        void WatchObjPosition() {
            if (transform.position.x < leftBorder) {
                OnOutBorder?.Invoke(SpawnFruitController.BorderType.Left);
            }

            if (transform.position.y < bottomBorder) {
                OnOutBorder?.Invoke(SpawnFruitController.BorderType.Bottom);
            }

            if (transform.position.x > rightBorder) {
                OnOutBorder?.Invoke(SpawnFruitController.BorderType.Right);
            }
        }

        float leftBorder;
        float rightBorder;
        float bottomBorder;

        protected virtual void Start()
        {
            gameManager = LevelManagerController.Instance;
            var spawnCtrl = gameManager.GetComponent<SpawnFruitController>();

            leftBorder = spawnCtrl.LeftBorder;
            rightBorder = spawnCtrl.RightBorder;
            bottomBorder = spawnCtrl.BottomBorder;
        }
        void Update()
        {
            WatchObjPosition();
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                var cursorController = gameManager.GetComponent<CursorController>();

                if (cursorController.IsDrawing)
                {
                    OnFruitDestroyed?.Invoke(gameObject);
                }
            }
        }

    }
}