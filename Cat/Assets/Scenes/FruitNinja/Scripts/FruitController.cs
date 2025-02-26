using UnityEngine;

public class FruitController : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private float speed = 15f;
    [SerializeField] private float torque = 0.1f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * speed, ForceMode.Impulse);
        rb.AddTorque(RandomTorque() * torque, ForceMode.Impulse);
    }

    private Vector3 RandomTorque() {
        return new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1));
    }
}
