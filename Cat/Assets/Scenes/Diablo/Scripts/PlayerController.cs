using Assets.Prefabs.Cat.Scripts;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Assets.Scenes.Diablo.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] AssetReference playerAsset;
        [SerializeField] GameObject startPosition;

        public GameObject PlayerPos => player == null ? startPosition : player;

        private GameObject player;
        private Vector3? targetPos;

        [SerializeField] private float movementThreshold = 0.01f;

        public void Rotate(Vector3 position)
        {
            if (player == null) return;

            targetPos = new Vector3(position.x, player.transform.position.y, position.z);
            player.transform.LookAt(targetPos ?? Vector3.zero);
        }

        public void Move()
        {
            if (targetPos == null) return;
            if (player == null) return;

            if (Vector3.Distance(player.transform.position, targetPos ?? Vector3.zero) > movementThreshold)
            {
                var agent = player.GetComponent<UnityEngine.AI.NavMeshAgent>();
                agent.SetDestination(targetPos ?? Vector3.zero);
                player.GetComponent<CatController>().Walk();
            }
            else
            {
                player.GetComponent<CatController>().Stand();
            }
        }

        void Awake()
        {
            playerAsset.InstantiateAsync(startPosition.transform.position, Quaternion.identity).Completed += handle =>
            {
                player = handle.Result;
                var catController = player.GetComponent<CatController>();
                catController.ChooseAnimationLayer(CatController.AnimationLayer.FIGHT);
            };
        }

    }

}
