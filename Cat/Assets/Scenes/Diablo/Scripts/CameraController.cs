using UnityEngine;

namespace Assets.Scenes.Diablo.Scripts
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Vector3 cameraRelativePos = new Vector3(0, 3, -1);
        private GameObject player;

        public void UpdateCameraPos(GameObject player)
        {
            transform.position = player.transform.position + cameraRelativePos;
            transform.LookAt(player.transform);
        }

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        void LateUpdate()
        {
            if (player) {
                UpdateCameraPos(player);
            }
        }
    }

}

