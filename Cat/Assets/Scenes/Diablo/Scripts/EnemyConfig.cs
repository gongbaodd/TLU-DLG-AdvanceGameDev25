using UnityEngine;

namespace Assets.Scenes.Diablo.Scripts
{
    [CreateAssetMenu(menuName = "Diablo/EnemyConfig")]
    public class EnemyConfig: ScriptableObject
    {
        public float waitTime;
        public float detectionRange = 10f;

        public float viewAngle = 120f;

        public float attackValue = 20f;

        public float attackRange = 2f;
    }
}