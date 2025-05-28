using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scenes.FruitNinja.Scripts
{
    public class SpawnFruitViewModel
    {
        private readonly GameConfig config;
        private readonly List<SpawnFruitModel> activeFruits = new();
        public List<SpawnFruitModel> ActiveFruits => activeFruits;

        public SpawnFruitViewModel(GameConfig config)
        {
            this.config = config;
        }

        public Vector2 CalculateForceDirection(Vector2 fruitPos, Vector2 targetPos)
        {
            Vector2 direction = (targetPos - fruitPos).normalized;
            return direction;
        }

        public Vector3 RandomSpawnPos()
        {
            return new Vector3(Random.Range(-config.spawnWidth, config.spawnWidth), config.spawnHeight, 0);
        }

        public bool ShouldSpawnBomb()
        {
            var bombWeight = config.bombWeight;
            var fruitWeight = config.fruitWeight;
            return Random.Range(0, bombWeight + fruitWeight) < bombWeight;
        }

        public void AddActiveFruit(SpawnFruitModel spawnable)
        {
            if (!activeFruits.Contains(spawnable))
                activeFruits.Add(spawnable);
        }

        public void RemoveActiveFruit(SpawnFruitModel spawnable)
        {
            activeFruits.Remove(spawnable);
        }
    }
} 