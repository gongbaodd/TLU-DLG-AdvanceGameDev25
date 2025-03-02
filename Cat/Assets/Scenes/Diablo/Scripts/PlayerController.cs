using Assets.Prefabs.Cat.Scripts;
using UnityEngine;

namespace Assets.Scenes.Diablo.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameObject _player;

        public GameObject player => _player;
        private Vector3? targetPos;

        [SerializeField] private float speed = 0.8f;
        [SerializeField] private float movementThreshold = 0.01f;

        public void Rotate(Vector3 position)
        {
            targetPos = new Vector3(position.x, _player.transform.position.y, position.z);
            _player.transform.LookAt(targetPos ?? Vector3.zero);
        }

        public void Move()
        {
            if (targetPos == null) return;

            if (Vector3.Distance(_player.transform.position, targetPos ?? Vector3.zero) > movementThreshold)
            {
                var agent = _player.GetComponent<UnityEngine.AI.NavMeshAgent>();
                agent.SetDestination(targetPos ?? Vector3.zero);
                _player.GetComponent<CatController>().Walk();
            }
            else
            {
                _player.GetComponent<CatController>().Stand();
            }
        }
        void Start()
        {
            if (_player == null)
            {
                Debug.LogError("Player not found");
            }

            _player.GetComponent<CatController>().ChooseAnimationLayer(CatController.AnimationLayer.FIGHT);
        }
    }

}
