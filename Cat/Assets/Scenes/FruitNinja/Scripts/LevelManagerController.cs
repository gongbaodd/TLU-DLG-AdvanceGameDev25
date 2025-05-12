using System.Collections;
using UnityEngine;

namespace Assets.Scenes.FruitNinja.Scripts
{

    [RequireComponent(
        typeof(SpawnFruitController),
        typeof(CursorController)
    )]
    public class LevelManagerController: MonoBehaviour
    {
        public static LevelManagerController Instance;
        AudioSource soundPlayer;
        [SerializeField] Item memoryItem;
        public float vfxTime = .6f;
        [SerializeField] GameObject boss;

        public void Win()
        {
            IEnumerator WinRoutine() {
                boss.SetActive(false);

                var inventory = Inventory.instance;
                inventory.Add(memoryItem);
                PlayWinSound();

                yield return new WaitForSeconds(vfxTime);
            }

            StartCoroutine(WinRoutine());
        }

        public void Lose()
        {
            IEnumerator LoseRoutine() {
                PlayFailSound();
                yield return new WaitForSeconds(vfxTime);
            }

            StartCoroutine(LoseRoutine());
        }
        [SerializeField] AudioClip spawnSound;
        public void PlaySpawnSound() => soundPlayer.PlayOneShot(spawnSound);
        [SerializeField] AudioClip poofSound;
        public void PlayPoofSound() => soundPlayer.PlayOneShot(poofSound);
        [SerializeField] AudioClip boomSound;
        public void PlayBoomSound() => soundPlayer.PlayOneShot(boomSound);
        [SerializeField] AudioClip outBorderSound;
        public void PlayOutBorderSound() => soundPlayer.PlayOneShot(outBorderSound);
        [SerializeField] AudioClip failSound;
        public void PlayFailSound() => soundPlayer.PlayOneShot(failSound);
        [SerializeField] AudioClip winSound;
        public void PlayWinSound() => soundPlayer.PlayOneShot(winSound);

        void Awake()
        {
            Instance = this;
            soundPlayer = GetComponent<AudioSource>();

            boss.GetComponentInChildren<Animator>().SetTrigger("Soccer");
        }
        void OnDestroy()
        {
            Instance = null;
        }
    }

}
