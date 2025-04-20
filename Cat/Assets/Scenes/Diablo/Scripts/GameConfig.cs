using UnityEngine;


namespace Assets.Scenes.Diablo.Scripts
{
    [CreateAssetMenu(menuName = "Diablo/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        public string boxCursorLabel = "Check it";
        public string enemyCursorLabel = "Attack";
    }
}