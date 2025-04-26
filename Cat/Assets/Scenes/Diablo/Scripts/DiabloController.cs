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

        public static GameConfig config;

        [SerializeField] GameConfig gameConfig;

        public void Win()
        {
            throw new System.NotImplementedException("Need to addItem to Inventory! Wait the Inventory to be implemented!");
        }

        public void Lose()
        {
            throw new System.NotImplementedException("Need to addItem to Inventory! Wait the Inventory to be implemented!");
        }

        void Awake()
        {
            gameManager = gameObject;
            player = GameObject.FindGameObjectWithTag("Player");

            if (gameConfig == null) {
                throw new System.Exception("Spawnables config not assigned. Please assign a Spawnables object in the inspector.");
            }
            
            config = gameConfig;
        }

        void OnDestroy()
        {
            gameManager = null;
            player = null;
        }
    }

}
