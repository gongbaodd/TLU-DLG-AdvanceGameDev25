using UnityEngine;

namespace Assets.Scenes.Diablo.Scripts
{
    [RequireComponent(
        typeof(PlayerController),
        typeof(CursorController)
    )]
    public class DiabloController : MonoBehaviour
    {
        public static GameObject gameManager;
        public static GameObject player;

        void Awake()
        {
            gameManager = gameObject;
            player = GameObject.FindGameObjectWithTag("Player");
        }

        void OnDestroy()
        {
            gameManager = null;
            player = null;
        }
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
    }

}
