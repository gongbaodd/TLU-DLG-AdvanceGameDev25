using System.Collections;
using UnityEngine;

namespace Assets.Scenes.Diablo.Scripts
{
    [RequireComponent(typeof(CursorController))]
    [RequireComponent(typeof(AudioManager))]
    [RequireComponent(typeof(LevelStateController))]
    public class LevelController : MonoBehaviour
    {
        public static LevelController Instance;
        public static GameObject player;
        public static GameConfig config;

        [SerializeField] GameConfig gameConfig;
        [SerializeField] GameObject MemoryFoundEffect;
        readonly float vfxTime = .6f;
        [SerializeField] Item memoryItem;        
        LevelStateController stateManager;
        [SerializeField] DialogController dialog;
        public void Win()
        {
            IEnumerator WinRoutine()
            {
                var inventory = Inventory.instance;
                inventory.Add(memoryItem);

                GetComponent<AudioManager>().PlayWinSound();
                MemoryFoundEffect.SetActive(true);

                dialog.Win();
                stateManager.StartStory();

                yield return new WaitForSeconds(vfxTime);
            }

            StartCoroutine(WinRoutine());

        }

        public void Lose()
        {

            IEnumerator LoseRoutine()
            {
                GetComponent<AudioManager>().PlayLoseSound();
                
                dialog.Lose();
                stateManager.StartStory();

                yield return new WaitForSeconds(vfxTime);
            }

            StartCoroutine(LoseRoutine());
        }

        public void NextScene()
        {
            SceneManagerController.Instance.GotoGardenScene();
        }

        public void StartGame()
        {
            stateManager.StartGame();
        }

        void Awake()
        {
            Instance = this;
            player = GameObject.FindGameObjectWithTag("Player");
            stateManager = GetComponent<LevelStateController>();

            if (gameConfig == null)
            {
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
