using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private List<GameObject> fruits;
    [SerializeField] private float spawnHeight = -5f;
    [SerializeField] private float spawnWidth = 10f;

    [SerializeField] private float spawnDelay = 1f;

    [SerializeField] private bool keepSpawning = true;

    void Start()
    {
        StartCoroutine(SpawnFruitRoutine());
    }

    IEnumerator SpawnFruitRoutine()
    {
        while (keepSpawning)
        {
            yield return new WaitForSeconds(spawnDelay);
            SpawnFruit();
        }
    }

    void SpawnFruit()
    {
        int index = Random.Range(0, fruits.Count);
        float width = spawnWidth - 1;
        Instantiate(fruits[index], new Vector3(Random.Range(-width, width), spawnHeight, 0), Quaternion.identity);
    }
}
