using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Scenes.FruitNinja.Scripts
{
    [RequireComponent(typeof(SpawnFruitController))]
    [RequireComponent(typeof(SpawnOutBorderController))]
    [RequireComponent(typeof(SpawnFruitEffectController))]
    public class SpawnFruitController : MonoBehaviour
    {
        [SerializeField] private GameObject target;

        [SerializeField] private GameConfig config;

        public GameConfig Config => config;

        private bool keepSpawning = true;

        private Coroutine spawnHandler;

        private SpawnFruitViewModel viewModel;

        IEnumerator SpawnFruitRoutine()
        {
            while (keepSpawning)
            {
                yield return new WaitForSeconds(config.spawnDelay);
                pool.Get();
            }
        }

        SpawnFruitModel InitSpawnable()
        {
            var gameCtrl = GetComponent<LevelManagerController>();
            var fruits = config.fruits;
            var bombs = config.bombs;

            var spawnable = new SpawnFruitModel();
            var effectController = GetComponent<SpawnFruitEffectController>();

            for (int i = 0; i < bombs.Count; i++)
            {
                var bomb = Instantiate(bombs[i], viewModel.RandomSpawnPos(), Quaternion.identity);
                var boomCtrl = bomb.GetComponent<FruitController>();

                boomCtrl.OnFruitDestroyed += obj =>
                {
                    effectController.PlayBoom(obj.transform.position);
                    pool.Release(spawnable);
                    gameCtrl.Lose();
                };

                bomb.SetActive(false);

                spawnable.bombs.Add(bomb);
            }

            for (int i = 0; i < fruits.Count; i++)
            {
                var fruit = Instantiate(fruits[i], viewModel.RandomSpawnPos(), Quaternion.identity);
                var fruitCtrl = fruit.GetComponent<FruitController>();

                fruitCtrl.OnFruitDestroyed += obj =>
                {
                    effectController.PlayPoof(obj.transform.position);
                    pool.Release(spawnable);
                    viewModel.RemoveActiveFruit(spawnable);
                };

                fruit.SetActive(false);

                spawnable.fruits.Add(fruit);
            }

            return spawnable;
        }

        void SpawnOne(SpawnFruitModel spawnable)
        {
            bool isSpawnBomb = viewModel.ShouldSpawnBomb();

            if (isSpawnBomb)
            {
                int index = Random.Range(0, spawnable.bombs.Count);
                spawnable.currentObj = spawnable.bombs[index];
                spawnable.currentObj.SetActive(true);
            }
            else
            {
                int index = Random.Range(0, spawnable.fruits.Count);
                spawnable.currentObj = spawnable.fruits[index];
                spawnable.currentObj.SetActive(true);
                // Track active fruit
                viewModel.AddActiveFruit(spawnable);
            }

            // Calculate force and torque
            var fruitCtrl = spawnable.currentObj.GetComponent<FruitController>();
            var speed = fruitCtrl.Speed;
            var torqueAmount = fruitCtrl.Torque;
            var force = viewModel.CalculateForceDirection(spawnable.currentObj.transform.position, target.transform.position) * speed;
            var torque = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * torqueAmount;

            // Play spawn audio
            var gameManager = GetComponent<LevelManagerController>();
            var audioController = gameManager.GetComponent<AudioController>();
            audioController.PlaySpawnSound();

            // Call new Spawn method
            fruitCtrl.Spawn(force, torque);
        }

        void RetrieveOne(SpawnFruitModel spawnable)
        {
            var fruit = spawnable.currentObj;

            var rb = fruit.GetComponent<Rigidbody>();
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            fruit.transform.position = viewModel.RandomSpawnPos();
            fruit.SetActive(false);

            // Remove from active fruits if present
            viewModel.RemoveActiveFruit(spawnable);

            spawnable.currentObj = null;
        }

        void DestoryOne(SpawnFruitModel spawnable)
        {
            Destroy(spawnable.currentObj);

            foreach (var fruit in spawnable.fruits)
            {
                Destroy(fruit);
            }

            foreach (var bomb in spawnable.bombs)
            {
                Destroy(bomb);
            }
        }

        private void StopSpawning()
        {
            keepSpawning = false;
            StopCoroutine(spawnHandler);
        }

        LinkedPool<SpawnFruitModel> pool;
        void InitPool()
        {
            pool = new(
                InitSpawnable,
                SpawnOne,
                RetrieveOne,
                DestoryOne,
                true,
                config.poolSize
            );
        }

        void ToggleSpawnByState(LevelStateController.State state)
        {
            switch (state)
            {
                case LevelStateController.State.Story:
                    StopSpawning();
                    break;
                case LevelStateController.State.Game:
                    keepSpawning = true;
                    spawnHandler = StartCoroutine(SpawnFruitRoutine());
                    break;
            }
        }
        public void ReleaseFruit(SpawnFruitModel fruit)
        {
            viewModel.RemoveActiveFruit(fruit);
            pool.Release(fruit);
        }

        void Awake()
        {
            viewModel = new SpawnFruitViewModel(config);
        }

        void Start()
        {
            InitPool();

            TimerController.OnTimerEnd += StopSpawning;
            LevelStateController.OnStateChange += ToggleSpawnByState;
        }

        void Update()
        {
            // Delegate out-of-border check to SpawnOutBorderController
            var outBorderCtrl = GetComponent<SpawnOutBorderController>();
            outBorderCtrl.CheckFruitsOutOfBorder(viewModel.ActiveFruits);
        }

        void OnDestroy()
        {
            TimerController.OnTimerEnd -= StopSpawning;
            LevelStateController.OnStateChange -= ToggleSpawnByState;
        }
    }
}
