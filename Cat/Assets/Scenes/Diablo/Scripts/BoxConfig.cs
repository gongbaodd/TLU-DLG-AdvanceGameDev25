using UnityEngine;

namespace Assets.Scenes.Diablo.Scripts
{

    [CreateAssetMenu(menuName = "Diablo/BoxConfig")]
    public class BoxConfig : ScriptableObject
    {
        public BoxContent content;

    }

    public enum BoxContent
    {
        None,
        Memory,
        Monster,
        Health,
    }

}