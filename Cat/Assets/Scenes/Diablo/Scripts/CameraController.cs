using UnityEngine;

namespace Assets.Scenes.Diablo.Scripts
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Vector3 cameraRelativePos = new(0, 3, -1);

        public void UpdateCameraPos(GameObject player)
        {
            transform.position = player.transform.position + cameraRelativePos;
            transform.LookAt(player.transform);
        }

        void LateUpdate()
        {
            if (LevelController.player) {
                UpdateCameraPos(LevelController.player);
            }
        }
    }

}

