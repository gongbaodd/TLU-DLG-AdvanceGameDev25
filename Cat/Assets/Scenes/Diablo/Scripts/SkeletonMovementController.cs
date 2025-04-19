using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scenes.Diablo.Scripts
{
    [RequireComponent(typeof(NavMeshAgent), typeof(SkeletonAnimatorController))]
    public class SkeletonMovementController : MonoBehaviour
    {
        NavMeshAgent agent;
        SkeletonAnimatorController aniController;

        [SerializeField] float waitTime = 5f;
        [SerializeField] List<Transform> spawnPositions = new();

        float waitTimer = 0f;
        bool isWaiting = true;

        void PickNewDestination()
        {
            if (spawnPositions.Count == 0) return;

            Transform randomTarget = spawnPositions[Random.Range(0, spawnPositions.Count)];
            agent.SetDestination(randomTarget.position);
        }

        void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            aniController = GetComponent<SkeletonAnimatorController>();
        }

        void Start()
        {
            PickNewDestination();
        }

        void Update()
        {
            if (isWaiting)
            {
                aniController.Stand();
                waitTimer += Time.deltaTime;

                if (waitTimer >= waitTime)
                {
                    waitTimer = 0f;
                    isWaiting = false;
                    PickNewDestination();
                }
            }
            else
            {
                if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
                {
                    isWaiting = true;
                }
                else
                {
                    aniController.Walk();
                }
            }
        }
    }
}
