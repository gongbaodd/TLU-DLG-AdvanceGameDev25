using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scenes.FruitNinja.Scripts
{
    public class SpawnFruitController : MonoBehaviour
    {
        [SerializeField] private List<GameObject> fruits;
        [SerializeField] private float spawnHeight = -5f;
        [SerializeField] private float spawnWidth = 10f;

        [SerializeField] private float spawnDelay = 1f;

        [SerializeField] private bool keepSpawning = true;

        private IEnumerator SpawnFruitRoutine()
        {
            while (keepSpawning)
            {
                yield return new WaitForSeconds(spawnDelay);
                SpawnFruit();
            }
        }

        private void SpawnFruit()
        {
            int index = Random.Range(0, fruits.Count);
            float width = spawnWidth - 1;
            Instantiate(fruits[index], new Vector3(Random.Range(-width, width), spawnHeight, 0), Quaternion.identity);
        }

        void Start()
        {
            StartCoroutine(SpawnFruitRoutine());
        }
    }
}
