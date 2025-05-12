using System.Collections;
using UnityEngine;

namespace Assets.Scenes.Diablo.Scripts
{
    [RequireComponent(typeof(CursorController))]
    [RequireComponent(typeof(AudioManager))]
    public class LevelController : MonoBehaviour
    {
        public static LevelController Instance;
        public static GameObject player;
        public static GameConfig config;

        [SerializeField] GameConfig gameConfig;
        [SerializeField] GameObject MemoryFoundEffect;

        readonly float vfxTime = .6f;

        [SerializeField] Item memoryItem;

        public void Win()
        {
            IEnumerator WinRoutine() {
                var inventory = Inventory.instance;
                inventory.Add(memoryItem);

                GetComponent<AudioManager>().PlayWinSound();
                MemoryFoundEffect.SetActive(true);
                yield return new WaitForSeconds(vfxTime);
            }

            StartCoroutine(WinRoutine());
        }

        public void Lose()
        {
            IEnumerator LoseRoutine() {
                GetComponent<AudioManager>().PlayLoseSound();
                yield return new WaitForSeconds(vfxTime);
            }

            StartCoroutine(LoseRoutine());
        }

        void Awake()
        {
            Instance = this;
            player = GameObject.FindGameObjectWithTag("Player");

            if (gameConfig == null) {
                throw new System.Exception("Spawnables config not assigned. Please assign a Spawnables object in the inspector.");
            }
            
            config = gameConfig;

            MemoryFoundEffect.SetActive(false);
        }

        void OnDestroy()
        {
            Instance = null;
            player = null;
        }
    }

}
