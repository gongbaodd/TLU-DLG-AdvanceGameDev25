using UnityEngine;

namespace DiabloScene
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private GameObject mainCamera;
        [SerializeField] private Vector3 cameraRelativePos = new Vector3(0, 3, -1);


        public void UpdateCameraPos(GameObject player)
        {
            mainCamera.transform.position = player.transform.position + cameraRelativePos;
            mainCamera.transform.LookAt(player.transform);
        }

        void Start()
        {
            if (mainCamera == null)
            {
                Debug.LogError("Main Camera not found");
            }
        }
    }

}

