using UnityEngine;

namespace Assets.Scenes.Diablo.Scripts
{
    [RequireComponent(
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
    }

}
