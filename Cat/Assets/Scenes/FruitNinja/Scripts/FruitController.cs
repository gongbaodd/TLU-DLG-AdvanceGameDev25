using UnityEngine;

namespace Assets.Scenes.FruitNinja.Scripts
{

    public class FruitController : MonoBehaviour
    {
        private Rigidbody rb;

        private GameObject gameManager;

        [SerializeField] private float speed = 15f;
        [SerializeField] private float torque = 0.1f;
        void Start()
        {
            gameManager = GameObject.FindWithTag("GameController");

            if (gameManager == null)
            {
                throw new System.Exception("GameManager not found in the scene. Please add a GameManager object with the 'GameController' tag.");
            }

            rb = GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up * speed, ForceMode.Impulse);
            rb.AddTorque(RandomTorque() * torque, ForceMode.Impulse);
        }

        private Vector3 RandomTorque()
        {
            return new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1));
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                var cursorController = gameManager.GetComponent<CursorController>();
                if (cursorController.IsDrawing)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}