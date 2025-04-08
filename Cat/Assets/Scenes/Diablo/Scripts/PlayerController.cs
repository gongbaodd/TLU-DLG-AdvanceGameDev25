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

        private void Rotate(Vector3 position)
        {
            if (loadedPlayer == null) return;

            targetPos = new Vector3(position.x, transform.position.y, position.z);
            transform.LookAt(targetPos ?? Vector3.zero);
        }

        private void Move()
        {
            if (targetPos == null) return;
            if (loadedPlayer == null) return;

            if (Vector3.Distance(transform.position, targetPos ?? Vector3.zero) > movementThreshold)
            {
                var agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
                agent.SetDestination(targetPos ?? Vector3.zero);
                loadedPlayer.GetComponent<CatController>().Walk();
            }
            else
            {
                loadedPlayer.GetComponent<CatController>().Stand();
                loadedPlayer.transform.localPosition = Vector3.zero;
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
                        Rotate(hitPoint.Value);
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


