using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scenes.FruitNinja.Scripts
{
    public class SpawnFruitViewModelTests
    {
        private SpawnFruitViewModel viewModel;
        private GameConfig config;

        [SetUp]
        public void Setup()
        {
            config = new GameConfig
            {
                spawnWidth = 5f,
                spawnHeight = 10f,
                bombWeight = 1,
                fruitWeight = 9
            };
            viewModel = new SpawnFruitViewModel(config);
        }

        [Test]
        public void CalculateForceDirection_ReturnsNormalizedDirection()
        {
            // Arrange
            Vector2 fruitPos = new Vector2(0, 0);
            Vector2 targetPos = new Vector2(3, 4);

            // Act
            Vector2 result = viewModel.CalculateForceDirection(fruitPos, targetPos);

            // Assert
            Assert.AreEqual(0.6f, result.x, 0.01f);
            Assert.AreEqual(0.8f, result.y, 0.01f);
            Assert.AreEqual(1f, result.magnitude, 0.01f); // Normalized vector should have magnitude of 1
        }

        [Test]
        public void RandomSpawnPos_ReturnsPositionWithinBounds()
        {
            // Act
            Vector3 result = viewModel.RandomSpawnPos();

            // Assert
            Assert.GreaterOrEqual(result.x, -config.spawnWidth);
            Assert.LessOrEqual(result.x, config.spawnWidth);
            Assert.AreEqual(config.spawnHeight, result.y);
            Assert.AreEqual(0, result.z);
        }

        [Test]
        public void ShouldSpawnBomb_ReturnsTrueWithinProbability()
        {
            // Arrange
            int trueCount = 0;
            int totalTests = 1000;

            // Act
            for (int i = 0; i < totalTests; i++)
            {
                if (viewModel.ShouldSpawnBomb())
                    trueCount++;
            }

            // Assert
            float probability = (float)trueCount / totalTests;
            float expectedProbability = (float)config.bombWeight / (config.bombWeight + config.fruitWeight);
            Assert.AreEqual(expectedProbability, probability, 0.1f); // Allow 10% margin of error
        }

        [Test]
        public void AddActiveFruit_AddsFruitToList()
        {
            // Arrange
            var fruit = new SpawnFruitModel();

            // Act
            viewModel.AddActiveFruit(fruit);

            // Assert
            Assert.Contains(fruit, viewModel.ActiveFruits);
        }

        [Test]
        public void AddActiveFruit_DoesNotAddDuplicate()
        {
            // Arrange
            var fruit = new SpawnFruitModel();
            viewModel.AddActiveFruit(fruit);

            // Act
            viewModel.AddActiveFruit(fruit);

            // Assert
            Assert.AreEqual(1, viewModel.ActiveFruits.Count);
        }

        [Test]
        public void RemoveActiveFruit_RemovesFruitFromList()
        {
            // Arrange
            var fruit = new SpawnFruitModel();
            viewModel.AddActiveFruit(fruit);

            // Act
            viewModel.RemoveActiveFruit(fruit);

            // Assert
            Assert.IsFalse(viewModel.ActiveFruits.Contains(fruit));
        }
    }
}
