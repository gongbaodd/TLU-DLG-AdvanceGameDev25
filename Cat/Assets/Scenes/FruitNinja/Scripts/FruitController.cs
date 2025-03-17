using UnityEngine;

namespace Assets.Scenes.FruitNinja.Scripts
{

    public class FruitController : MonoBehaviour
    {
        private Rigidbody rb;

        private GameObject explosionFX;
        [SerializeField] private float speed = 15f;
        [SerializeField] private float torque = 0.1f;
        [SerializeField] private float destroyDelay = 2f;
        [SerializeField] private float destroyForce = 50f;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up * speed, ForceMode.Impulse);
            rb.AddTorque(RandomTorque() * torque, ForceMode.Impulse);

            explosionFX = GameObject.FindGameObjectWithTag("ExplosionFX");

            if (explosionFX == null)
            {
                Debug.LogError("ExplosionFX not found!");
            }
        }

        private Vector3 RandomTorque()
        {
            return new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("CatPaw"))
            {

                Vector3 forceDirection = (transform.position - other.transform.position).normalized;
                rb.AddForce(forceDirection * destroyForce, ForceMode.Impulse);

                explosionFX.GetComponent<ParticleSystem>().Play();

                rb.isKinematic = true;
                rb.useGravity = false;
                rb.detectCollisions = false;
                Destroy(gameObject, destroyDelay);
            }
        }
    }
}