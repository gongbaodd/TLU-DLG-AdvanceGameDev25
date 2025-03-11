using UnityEngine;

namespace Assets.Scenes.Diablo.Scripts
{
    [RequireComponent(
        typeof(PlayerController),
        typeof(CameraController),
        typeof(CursorController)
    )]
    public class DiabloController : MonoBehaviour
    {
        void Update()
        {
            var playerController = GetComponent<PlayerController>();
            var cursorController = GetComponent<CursorController>();
            if (Input.GetMouseButtonDown(0))
            {
                var hitPoint = cursorController.HitTest();

                if (hitPoint.HasValue)
                {
                    playerController.Rotate(hitPoint.Value);
                }
            }

            playerController.Move();
        }

        void LateUpdate()
        {
            var cameraController = GetComponent<CameraController>();
            var playerController = GetComponent<PlayerController>();
            cameraController.UpdateCameraPos(playerController.PlayerPos);
        }
    }

}
