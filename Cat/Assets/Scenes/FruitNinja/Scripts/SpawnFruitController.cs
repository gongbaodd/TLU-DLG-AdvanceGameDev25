using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scenes.FruitNinja.Scripts
{
    public class SpawnFruitController : MonoBehaviour
    {
        [SerializeField] private List<GameObject> fruits;

        [SerializeField] private GameObject target;
        [SerializeField] private GameObject poofPrefab;
        [SerializeField] private float spawnHeight = -5f;
        [SerializeField] private float spawnWidth = 10f;

        [SerializeField] private float spawnDelay = 1f;

        [SerializeField] private bool keepSpawning = true;

        public Vector2 CalculateForceDirection(Vector2 fruitPos) {
            Vector2 targetPos = target.transform.position;
            Vector2 direction = (targetPos - fruitPos).normalized;
            return direction;
        }

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
            var fruit = Instantiate(fruits[index], new Vector3(Random.Range(-width, width), spawnHeight, 0), Quaternion.identity);

            fruit.GetComponent<FruitController>().OnFruitDestroyed += Poof;
        }

        private void Poof(Vector2 fruitPos)
        {
            var poof = Instantiate(poofPrefab, new Vector3(fruitPos.x, fruitPos.y, 0), Quaternion.identity);

            poof.GetComponent<ParticleSystem>().Play();

            Destroy(poof, 1f);
        }

        void Start()
        {
            StartCoroutine(SpawnFruitRoutine());
        }
    }
}
