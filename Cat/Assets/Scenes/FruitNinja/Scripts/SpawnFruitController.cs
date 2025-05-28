using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Scenes.FruitNinja.Scripts
{
    [RequireComponent(typeof(SpawnFruitController))]
    public class SpawnFruitController : MonoBehaviour
    {
        [SerializeField] private GameObject target;

        [SerializeField] private GameConfig config;

        public enum BorderType
        {
            Left,
            Right,
            Bottom,
        }
        [SerializeField] private GameObject leftBorder;
        [SerializeField] private GameObject rightBorder;
        [SerializeField] private GameObject bottomBorder;
        [SerializeField] private GameObject dropOutMark;

        public float LeftBorder => leftBorder.transform.position.x;
        public float RightBorder => rightBorder.transform.position.x;
        public float BottomBorder => bottomBorder.transform.position.y;

        public GameConfig Config => config;

        private bool keepSpawning = true;

        private Coroutine spawnHandler;

        // Track all active fruits
        private List<SpawnFruitModel> activeFruits = new();

        public Vector2 CalculateForceDirection(Vector2 fruitPos)
        {
            Vector2 targetPos = target.transform.position;
            Vector2 direction = (targetPos - fruitPos).normalized;
            return direction;
        }

        IEnumerator SpawnFruitRoutine()
        {
            while (keepSpawning)
            {
                yield return new WaitForSeconds(config.spawnDelay);
                pool.Get();
            }
        }

        Vector3 RandomSpawnPos() => new(Random.Range(-config.spawnWidth, config.spawnWidth), config.spawnHeight, 0);

        void InitOutBoundaryMark(BorderType border, Vector3 position)
        {
            Vector3 pos;
            switch (border)
            {
                case BorderType.Left:
                    pos = new Vector3(leftBorder.transform.position.x + 4f, position.y, 0);
                    Instantiate(dropOutMark, pos, Quaternion.identity);
                    break;
                case BorderType.Right:
                    pos = new Vector3(rightBorder.transform.position.x - 4f, position.y, 0);
                    Instantiate(dropOutMark, pos, Quaternion.identity);
                    break;
                case BorderType.Bottom:
                    pos = new Vector3(position.x, bottomBorder.transform.position.y + 4f, 0);
                    Instantiate(dropOutMark, pos, Quaternion.identity);
                    break;
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
                var bomb = Instantiate(bombs[i], RandomSpawnPos(), Quaternion.identity);
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
                var fruit = Instantiate(fruits[i], RandomSpawnPos(), Quaternion.identity);
                var fruitCtrl = fruit.GetComponent<FruitController>();

                fruitCtrl.OnFruitDestroyed += obj =>
                {
                    effectController.PlayPoof(obj.transform.position);
                    pool.Release(spawnable);
                    activeFruits.Remove(spawnable);
                };

                fruit.SetActive(false);

                spawnable.fruits.Add(fruit);
            }

            return spawnable;
        }

        void SpawnOne(SpawnFruitModel spawnable)
        {
            var bombWeight = config.bombWeight;
            var fruitWeight = config.fruitWeight;

            bool isSpawnBomb = Random.Range(0, bombWeight + fruitWeight) < bombWeight;

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
                if (!activeFruits.Contains(spawnable))
                    activeFruits.Add(spawnable);
            }

            // Calculate force and torque
            var fruitCtrl = spawnable.currentObj.GetComponent<FruitController>();
            var speed = fruitCtrl.Speed;
            var torqueAmount = fruitCtrl.Torque;
            var force = CalculateForceDirection(spawnable.currentObj.transform.position) * speed;
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

            fruit.transform.position = RandomSpawnPos();
            fruit.SetActive(false);

            // Remove from active fruits if present
            activeFruits.Remove(spawnable);

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

        void Start()
        {
            InitPool();

            TimerController.OnTimerEnd += StopSpawning;
            LevelStateController.OnStateChange += ToggleSpawnByState;
        }

        void OnDestroy()
        {
            TimerController.OnTimerEnd -= StopSpawning;
            LevelStateController.OnStateChange -= ToggleSpawnByState;
        }

        void Update()
        {
            // Check all active fruits for out-of-bounds
            for (int i = activeFruits.Count - 1; i >= 0; i--)
            {
                var fruit = activeFruits[i];
                if (!fruit.currentObj.activeInHierarchy) continue;
                var pos = fruit.currentObj.transform.position;
                BorderType? outBorder = null;
                if (pos.x < LeftBorder) outBorder = BorderType.Left;
                else if (pos.x > RightBorder) outBorder = BorderType.Right;
                else if (pos.y < BottomBorder) outBorder = BorderType.Bottom;
                if (outBorder.HasValue)
                {
                    InitOutBoundaryMark(outBorder.Value, pos);
                    activeFruits.RemoveAt(i);
                    pool.Release(fruit);

                    var gameCtrl = GetComponent<LevelManagerController>();
                    gameCtrl.GetComponent<AudioController>().PlayOutBorderSound();
                    gameCtrl.Lose();
                }
            }
        }
    }
}
