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

        public void Win()
        {
            throw new System.NotImplementedException("Need to addItem to Inventory! Wait the Inventory to be implemented!");
        }

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
