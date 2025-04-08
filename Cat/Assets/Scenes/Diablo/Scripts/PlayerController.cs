using UnityEngine;
using UnityEngine.AddressableAssets;
using Assets.Prefabs.Cat.Scripts;

namespace Assets.Scenes.Diablo.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] AssetReference playerAsset;
        private GameObject loadedPlayer;
        private Vector3? targetPos;
        [SerializeField] private float movementThreshold = 0.01f;

        private void LoadAsset()
        {
            playerAsset.InstantiateAsync(transform.position, Quaternion.identity, transform).Completed += handle =>
            {
                loadedPlayer = handle.Result;
                var catController = loadedPlayer.GetComponent<CatController>();
                catController.ChooseAnimationLayer(CatController.AnimationLayer.FIGHT);
            };
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
            var gameManager = DiabloController.gameManager;
            if (gameManager)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    var cursorController = gameManager.GetComponent<CursorController>();
                    var hitPoint = cursorController.HitTest();

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
    }
}


