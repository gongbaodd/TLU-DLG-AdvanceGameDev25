using UnityEngine;
using UnityEngine.AddressableAssets;
using Assets.Prefabs.Cat.Scripts;

namespace Assets.Scenes.Diablo.Scripts
{
    [RequireComponent(typeof(LifeBarController))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] GameObject playerAsset;
        private GameObject loadedPlayer;
        private Vector3? targetPos;

        private void LoadAsset()
        {
            loadedPlayer = Instantiate(playerAsset, transform.position, Quaternion.identity, transform);
            var catController = loadedPlayer.GetComponent<CatController>();
            catController.ChooseAnimationLayer(CatController.AnimationLayer.FIGHT);
        }

        private void SetTarget(Vector3 position)
        {
            if (loadedPlayer == null) return;

            targetPos = new Vector3(position.x, transform.position.y, position.z);
        }

        private void Move()
        {
            if (targetPos == null) return;
            if (loadedPlayer == null) return;

            var agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

            if (!agent.pathPending) {
                agent.SetDestination(targetPos ?? Vector3.zero);

                if (agent.remainingDistance > agent.stoppingDistance) {
                    loadedPlayer.GetComponent<CatController>().Walk();
                } else {
                    loadedPlayer.GetComponent<CatController>().Stand();
                }
            }
        }

        private void UpdateMovement()
        {
            var gameManager = LevelController.Instance;
            if (gameManager)
            {
                var state = gameManager.GetComponent<LevelStateController>().currentState;
                if (state != LevelStateController.State.Game) return;

                if (Input.GetMouseButtonDown(0))
                {
                    var cursorController = gameManager.GetComponent<CursorController>();
                    var hitPoint = cursorController.HitPoint();

                    if (hitPoint.HasValue)
                    {
                        SetTarget(hitPoint.Value);
                    }
                }
            }

            Move();
        }


        void Awake()
        {
            LoadAsset();
        }

        void Update()
        {
            UpdateMovement();
        }

        void OnDestroy()
        {
            Destroy(loadedPlayer);
        }
    }
}


