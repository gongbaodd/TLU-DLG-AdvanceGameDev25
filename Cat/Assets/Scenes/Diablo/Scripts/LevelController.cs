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
        bool finished = false;
        public void Win()
        {
            IEnumerator WinRoutine()
            {
                finished = true;
                var inventory = Inventory.instance;
                inventory.Add(memoryItem);

                GetComponent<AudioManager>().PlayWinSound();
                MemoryFoundEffect.SetActive(true);

                yield return new WaitForSeconds(vfxTime);


                dialog.Win();
                stateManager.StartStory();
            }
            if (finished == false)
            {
                StartCoroutine(WinRoutine());
            }
        }

        public void Lose()
        {

            IEnumerator LoseRoutine()
            {
                finished = true;

                GetComponent<AudioManager>().PlayLoseSound();

                yield return new WaitForSeconds(vfxTime);

                dialog.Lose();
                stateManager.StartStory();
            }

            if (finished == false)
            {
                StartCoroutine(LoseRoutine());
            }

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
