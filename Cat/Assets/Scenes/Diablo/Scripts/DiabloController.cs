using System.Collections;
using UnityEngine;

namespace Assets.Scenes.Diablo.Scripts
{
    [RequireComponent(
        typeof(CursorController)
    )]
    public class LevelController : MonoBehaviour
    {
        public static LevelController Instance;
        public static GameObject player;

        public static GameConfig config;

        AudioSource sound;
        [SerializeField] AudioClip boxBiteSound;
        public void PlayBoxBiteSound() => sound.PlayOneShot(boxBiteSound);
        [SerializeField] AudioClip enemyPunchSound;
        public void PlayEnemyPunchSound() => sound.PlayOneShot(enemyPunchSound);
        [SerializeField] GameConfig gameConfig;
        [SerializeField] GameObject MemoryFoundEffect;

        [SerializeField] AudioClip winSound;
        public void PlayWinSound() => sound.PlayOneShot(winSound);
        [SerializeField] AudioClip loseSound;
        public void PlayLoseSound() => sound.PlayOneShot(loseSound);

        readonly float vfxTime = .6f;

        [SerializeField] Item memoryItem;

        public void Win()
        {
            IEnumerator WinRoutine() {
                var inventory = Inventory.instance;
                inventory.Add(memoryItem);

                PlayWinSound();
                MemoryFoundEffect.SetActive(true);
                yield return new WaitForSeconds(vfxTime);
            }

            StartCoroutine(WinRoutine());
        }

        public void Lose()
        {
            IEnumerator LoseRoutine() {
                PlayLoseSound();
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
            sound = GetComponent<AudioSource>();

            MemoryFoundEffect.SetActive(false);
        }

        void OnDestroy()
        {
            Instance = null;
            player = null;
        }
    }

}
