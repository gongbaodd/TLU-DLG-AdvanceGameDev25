using UnityEngine;

namespace Assets.Scenes.Diablo.Scripts
{
    [RequireComponent(typeof(BoxCollider))]
    public class EnemyVisionDetectorController : MonoBehaviour
    {
        GameObject Target;

        public GameObject GetTarget() => Target;

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) {
                Target = other.gameObject;
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player")) {
                Target = null;
            }
        }
    }

}

