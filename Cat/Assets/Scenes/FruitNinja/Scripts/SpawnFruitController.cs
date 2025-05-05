using System.Collections;
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

        private IEnumerator spawnHandler;

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

        GameObject InitFruit()
        {
            var gameCtrl = GetComponent<FruitNinjaController>();
            var fruits = config.fruits;
            var bombs = config.bombs;

            var bombWeight = config.bombWeight;
            var fruitWeight = config.fruitWeight;

            var spawnWidth = config.spawnWidth;
            var spawnHeight = config.spawnHeight;

            bool isSpawnBomb = Random.Range(0, bombWeight + fruitWeight) < bombWeight ? true : false;

            if (isSpawnBomb)
            {
                int index = Random.Range(0, bombs.Count);
                var spawnable = Instantiate(bombs[index], RandomSpawnPos(), Quaternion.identity);
                var boomCtrl = spawnable.GetComponent<FruitController>();

                boomCtrl.OnFruitDestroyed += obj => {
                    Boom(obj.transform.position);
                    pool.Release(obj);

                    gameCtrl.Lose();
                };

                boomCtrl.OnOutBorder += borderType => {
                    pool.Release(spawnable);
                };

                return spawnable;
            }
            else
            {
                int index = Random.Range(0, fruits.Count);
                var spawnable = Instantiate(fruits[index], RandomSpawnPos(), Quaternion.identity);
                var fruitCtrl = spawnable.GetComponent<FruitController>();

                fruitCtrl.OnFruitDestroyed += obj => {
                    Poof(obj.transform.position);
                    pool.Release(obj);
                };

                fruitCtrl.OnOutBorder += borderType => {
                    InitOutBoundaryMark(borderType, spawnable.transform.position);
                    pool.Release(spawnable);

                    gameCtrl.Lose();
                };

                return spawnable;
            }
        }

        private void Poof(Vector2 fruitPos)
        {
            var poofPrefab = config.poofPrefab;
            var poof = Instantiate(poofPrefab, new Vector3(fruitPos.x, fruitPos.y, 0), Quaternion.identity);

            poof.GetComponent<ParticleSystem>().Play();

            Destroy(poof, 1f);
        }

        private void Boom(Vector2 boomPos) {
            var boomPrefab = config.boomPrefab;
            var boom = Instantiate(boomPrefab, new Vector3(boomPos.x, boomPos.y, 0), Quaternion.identity);

            boom.GetComponent<ParticleSystem>().Play();
            Destroy(boom, 1f);
        }

        private void StopSpawning()
        {
            keepSpawning = false;
            StopCoroutine(spawnHandler);
        }

        LinkedPool<GameObject> pool;
        void InitPool() {
            pool = new LinkedPool<GameObject>(
                () => {
                    var fruit = InitFruit();
                    fruit.SetActive(false);
                    return fruit;
                },
                fruit =>
                {
                    fruit.SetActive(true);
                    fruit.GetComponent<FruitController>().Spawn();
                },
                fruit =>
                {
                    var rb = fruit.GetComponent<Rigidbody>();
                    rb.linearVelocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;

                    fruit.transform.position = RandomSpawnPos();
                    fruit.SetActive(false);
                },
                fruit => Destroy(fruit),
                true,
                config.poolSize
            );
        }

        void Start()
        {
            spawnHandler = SpawnFruitRoutine();
            StartCoroutine(spawnHandler);

            InitPool();

            TimerController.OnTimerEnd += StopSpawning;
        }

        void OnDestroy()
        {
            TimerController.OnTimerEnd -= StopSpawning;
        }
    }
}
