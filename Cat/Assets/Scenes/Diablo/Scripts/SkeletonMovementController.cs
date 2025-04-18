using UnityEngine;
using UnityEngine.AI;


namespace Assets.Scenes.Diablo.Scripts
{
    [RequireComponent(typeof(NavMeshAgent), typeof(SkeletonAnimatorController))]
    public class SkeletonMovementController : MonoBehaviour
    {
        NavMeshAgent agent;
        SkeletonAnimatorController aniController;
        float timer = 0f;

        [SerializeField] float wanderRadius = 50f;
        [SerializeField] float waitTime = 5f;

        void SetRandomDestination()
        {
            Vector3 randomDestination = transform.position + new Vector3(Random.insideUnitCircle.x, 0, Random.insideUnitCircle.y) * wanderRadius;

            NavMeshHit hit;

            if (NavMesh.SamplePosition(randomDestination, out hit, wanderRadius, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
            }
        }

        void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            aniController = GetComponent<SkeletonAnimatorController>();
        }

        void Update()
        {
            if (!agent.pathPending) {
                SetRandomDestination();
                if (agent.remainingDistance <= agent.stoppingDistance) {
                    aniController.Stand();
                    
                    timer += Time.deltaTime;
                    if (timer > waitTime) {
                        timer = 0f;
                    }    

                } else {
                    aniController.Walk();
                }
            }
        }
    }
}

