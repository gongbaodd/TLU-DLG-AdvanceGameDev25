using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scenes.FruitNinja.Scripts
{
    [CreateAssetMenu(fileName = "config", menuName = "FruitNinja/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        public List<GameObject> fruits;
        public List<GameObject> bombs;
        public GameObject poofPrefab;
        public float spawnHeight = -5f;
        public float spawnWidth = 10f;
        public float spawnDelay = 1f;
    }

}
