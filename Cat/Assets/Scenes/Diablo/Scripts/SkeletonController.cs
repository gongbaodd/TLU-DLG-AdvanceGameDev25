using UnityEngine;

namespace Assets.Scenes.Diablo.Scripts
{
    [RequireComponent(typeof(SkeletonAnimatorController))]
    [RequireComponent(typeof(CursorLabelController))]
    [RequireComponent(typeof(EnemyVisionDetectorController))]
    public class SkeletonController : MonoBehaviour
    {
        public EnemyConfig config;
    }
}

