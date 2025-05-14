using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Scenes.FruitNinja.Scripts
{
    public class SpawnFruitController : MonoBehaviour
    {
        [SerializeField] private GameObject target;

        [SerializeField] private GameConfig config;

        public enum BorderType {
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

        Vector3 RandomSpawnPos() => new (Random.Range(-config.spawnWidth, config.spawnWidth), config.spawnHeight, 0);

        void InitOutBoundaryMark(BorderType border, Vector3 position) {
            Vector3 pos;
            switch (border) {
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

        class Spawnable {
            public List<GameObject> fruits = new ();
            public List<GameObject> bombs = new ();
            public GameObject currentObj;
        }

        Spawnable InitSpawnable()
        {
            var gameCtrl = GetComponent<LevelManagerController>();
            var fruits = config.fruits;
            var bombs = config.bombs;

            var spawnable = new Spawnable();

            for (int i =0; i < bombs.Count; i++) {
                var bomb = Instantiate(bombs[i], RandomSpawnPos(), Quaternion.identity);
                var boomCtrl = bomb.GetComponent<FruitController>();

                boomCtrl.OnFruitDestroyed += obj => {
                    Boom(obj.transform.position);
                    pool.Release(spawnable);
                    gameCtrl.Lose();
                };

                bomb.SetActive(false);

                spawnable.bombs.Add(bomb);
            }

            for (int i =0; i < fruits.Count; i++) {
                var fruit = Instantiate(fruits[i], RandomSpawnPos(), Quaternion.identity);
                var fruitCtrl = fruit.GetComponent<FruitController>();

                fruitCtrl.OnFruitDestroyed += obj => {
                    Poof(obj.transform.position);
                    pool.Release(spawnable);
                };

                fruitCtrl.OnOutBorder += borderType => {
                    InitOutBoundaryMark(borderType, fruit.transform.position);
                    pool.Release(spawnable);
                    gameCtrl.GetComponent<AudioController>().PlayOutBorderSound();
                    gameCtrl.Lose();
                };

                fruit.SetActive(false);

                spawnable.fruits.Add(fruit);
            }

            return spawnable;
        }

        void SpawnOne(Spawnable spawnable) 
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
            }

            spawnable.currentObj.GetComponent<FruitController>().Spawn();
        }

        void RetrieveOne(Spawnable spawnable)
        {
            var fruit = spawnable.currentObj;
            
            var rb = fruit.GetComponent<Rigidbody>();
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            fruit.transform.position = RandomSpawnPos();
            fruit.SetActive(false);

            spawnable.currentObj = null;
        }

        void DestoryOne(Spawnable spawnable)
        {
            Destroy(spawnable.currentObj);

            foreach (var fruit in spawnable.fruits) {
                Destroy(fruit);
            }

            foreach (var bomb in spawnable.bombs) {
                Destroy(bomb);
            }
        }

        private void Poof(Vector2 fruitPos)
        {
            var poofPrefab = config.poofPrefab;
            var poof = Instantiate(poofPrefab, new Vector3(fruitPos.x, fruitPos.y, 0), Quaternion.identity);
            poof.GetComponent<ParticleSystem>().Play();

            var gameCtrl = GetComponent<LevelManagerController>();
            gameCtrl.GetComponent<AudioController>().PlayPoofSound();

            Destroy(poof, 1f);
        }

        private void Boom(Vector2 boomPos) {
            var boomPrefab = config.boomPrefab;
            var boom = Instantiate(boomPrefab, new Vector3(boomPos.x, boomPos.y, 0), Quaternion.identity);

            boom.GetComponent<ParticleSystem>().Play();

            var gameCtrl = GetComponent<LevelManagerController>();
            gameCtrl.GetComponent<AudioController>().PlayBoomSound();

            Destroy(boom, 1f);
        }

        private void StopSpawning()
        {
            keepSpawning = false;
            StopCoroutine(spawnHandler);
        }

        LinkedPool<Spawnable> pool;
        void InitPool() {
            pool = new (
                InitSpawnable,
                SpawnOne,
                RetrieveOne,
                DestoryOne,
                true,
                config.poolSize
            );
        }

        void ToggleSpawnByState(LevelStateController.State state) {
            switch (state) {
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
    }
}
