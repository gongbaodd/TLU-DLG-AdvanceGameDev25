using UnityEngine;

namespace Assets.Scenes.Diablo.Scripts
{
    [RequireComponent(typeof(SkeletonMovementController), typeof(SkeletonAnimatorController))]
    [RequireComponent(typeof(CursorLabelController), typeof(EnemyStateController))]
    public class SkeletonController : MonoBehaviour
    {
        public EnemyConfig config;
    }
}

