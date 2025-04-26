using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scenes.Diablo.Scripts
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(BoxCollider))]
    public class SkeletonMovementController : MonoBehaviour
    {
        NavMeshAgent agent;
        [SerializeField] List<Transform> spawnPositions = new();

        GameObject player;

        public void PickNewDestination()
        {
            if (spawnPositions.Count == 0) return;

            Transform randomTarget = spawnPositions[Random.Range(0, spawnPositions.Count)];
            agent.SetDestination(randomTarget.position);
        }


        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) {
                player = other.gameObject;
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player")) {
                player = null;
            }
        }

        public bool IsWalking() {
            if (agent.pathPending) {
                return true;
            }

            if (agent.remainingDistance > agent.stoppingDistance) {
                return true;
            }

            return false;
        }

        void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }
    }
}
