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

        public float Speed
        {
            get { return config.speed; }
            private set { config.speed = value; }
        }

        public float Torque
        {
            get { return config.torque; }
            private set { config.torque = value; }
        }

        public void Spawn(Vector3 force, Vector3 torque)
        {
            rb = GetComponent<Rigidbody>();
            rb.AddForce(force, ForceMode.Impulse);
            rb.AddTorque(torque, ForceMode.Impulse);
        }

        private Vector3 RandomTorque()
        {
            return new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1));
        }

        void Start()
        {
            gameManager = LevelManagerController.Instance;
        }

        void OnTriggerEnter(Collider other)
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