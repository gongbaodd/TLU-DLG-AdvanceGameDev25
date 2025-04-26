using UnityEngine;

namespace Assets.Scenes.Diablo.Scripts
{

    [CreateAssetMenu(menuName = "Diablo/BoxConfig")]
    public class BoxConfig : ScriptableObject
    {
        public BoxContent content;

        public float attackInterval = 1f;

        public float attackValue = 10f;

        public float attackRange = 2f;

    }

    public enum BoxContent
    {
        None,
        Memory,
        Monster,
        Health,
    }

}