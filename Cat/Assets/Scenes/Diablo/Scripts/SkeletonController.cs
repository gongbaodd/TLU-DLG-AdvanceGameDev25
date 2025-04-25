using UnityEngine;

namespace Assets.Scenes.Diablo.Scripts
{
    [RequireComponent(typeof(SkeletonAnimatorController))]
    [RequireComponent(typeof(CursorLabelController))]
    public class SkeletonController : MonoBehaviour
    {
        public EnemyConfig config;

        // void DetectPlayer()
        // {
        //     Collider[] hits = Physics.OverlapSphere(transform.position, config.detectionRange, LayerMask.GetMask("Player"));

        //     Gizmos.color = Color.yellow;
        //     // Gizmos.DrawWireSphere(transform.position, config.detectionRange);

        //     foreach (Collider hit in hits)
        //     {
        //         Transform target = hit.transform;
        //         Vector3 direction = (target.position - transform.position).normalized;

        //         if (Vector3.Angle(transform.forward, direction) < config.viewAngle / 2)
        //         {
        //             print("player detected");
        //         }
        //     }
        // }

        void Update()
        {
            // DetectPlayer();
        }
    }
}

